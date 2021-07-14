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
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<RamMetricJob> _logger;
        private readonly IRamMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        public RamMetricJob(IRamMetricsRepository metricsRepository,
            IAgentsRepository agentsRepository,
            ILogger<RamMetricJob> logger,
            IRamMetricsAgentClient agentClient,
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
                var response = await _agentClient.GetMetrics(new RamMetricClientRequest
                {
                    BaseUrl = agentInfo.AgentUrl,
                    FromTime = lastTime,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null)
                    return;

                foreach (var clientMetric in response.Metrics)
                {
                    var RamMetric = _mapper.Map<RamMetric>(clientMetric);
                    RamMetric.AgentId = agentInfo.AgentId;
                    _metricsRepository.Create(RamMetric);
                }

                _logger.LogDebug($"Sincronized {response.Metrics.Count} Ram Metrics from Agent ({agentInfo})");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}