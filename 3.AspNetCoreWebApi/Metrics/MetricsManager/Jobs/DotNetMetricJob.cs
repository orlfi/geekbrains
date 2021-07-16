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
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<DotNetMetricJob> _logger;
        private readonly IDotNetMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        public DotNetMetricJob(
            IDotNetMetricsRepository metricsRepository,
            IAgentsRepository agentsRepository,
            ILogger<DotNetMetricJob> logger,
            IDotNetMetricsAgentClient agentClient,
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
                var response = await _agentClient.GetMetrics(new DotNetMetricClientRequest
                {
                    BaseUrl = agentInfo.AgentUrl,
                    FromTime = lastTime,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null)
                    return;

                foreach (var clientMetric in response.Metrics)
                {
                    var DotNetMetric = _mapper.Map<DotNetMetric>(clientMetric);
                    DotNetMetric.AgentId = agentInfo.AgentId;
                    _metricsRepository.Create(DotNetMetric);
                }

                _logger.LogDebug($"Sincronized {response.Metrics.Count} DotNet Metrics from Agent ({agentInfo})");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}