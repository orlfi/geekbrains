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
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<NetworkMetricJob> _logger;
        private readonly INetworkMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        public NetworkMetricJob(
            INetworkMetricsRepository metricsRepository, 
            IAgentsRepository agentsRepository, 
            ILogger<NetworkMetricJob> logger, 
            INetworkMetricsAgentClient agentClient, 
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
                var response = await _agentClient.GetMetrics(new NetworkMetricClientRequest
                {
                    BaseUrl = agentInfo.AgentUrl,
                    FromTime = lastTime,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null)
                    return;

                foreach (var clientMetric in response.Metrics)
                {
                    var NetworkMetric = _mapper.Map<NetworkMetric>(clientMetric);
                    NetworkMetric.AgentId = agentInfo.AgentId;
                    _metricsRepository.Create(NetworkMetric);
                }

                _logger.LogDebug($"Sincronized {response.Metrics.Count} Network Metrics from Agent ({agentInfo})");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}