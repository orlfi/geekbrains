using System;
using Quartz;
using System.Threading.Tasks;
using System.Linq;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;
using MetricsManager.ApiClients.Interfaces;
using MetricsManager.ApiClients.Requests;
using AutoMapper;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<HddMetricJob> _logger;
        private readonly IHddMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        public HddMetricJob(
            IHddMetricsRepository metricsRepository,
            IAgentsRepository agentsRepository,
            ILogger<HddMetricJob> logger,
            IHddMetricsAgentClient agentClient,
            IMapper mapper)
        {
            _metricsRepository = metricsRepository;
            _agentsRepository = agentsRepository;
            _logger = logger;
            _agentClient = agentClient;
            _mapper = mapper;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var enabledAgents = _agentsRepository.GetRegistered().Where(item => item.IsEnabled);
            foreach (var agent in enabledAgents)
            {
                await SyncronizeMetricsFromAgent(agent);
            }
        }

        private async Task SyncronizeMetricsFromAgent(AgentInfo agentInfo)
        {
            try
            {
                var lastTime = _metricsRepository.GetMetricsLastDateFormAgent(agentInfo.AgentId).AddSeconds(1);
                var response = await _agentClient.GetMetrics(new HddMetricClientRequest
                {
                    BaseUrl = agentInfo.AgentUrl,
                    FromTime = lastTime,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null)
                    return;

                foreach (var clientMetric in response.Metrics)
                {
                    var HddMetric = _mapper.Map<HddMetric>(clientMetric);
                    HddMetric.AgentId = agentInfo.AgentId;
                    _metricsRepository.Create(HddMetric);
                }

                _logger.LogDebug($"Sincronized {response.Metrics.Count} Hdd Metrics from Agent ({agentInfo})");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}