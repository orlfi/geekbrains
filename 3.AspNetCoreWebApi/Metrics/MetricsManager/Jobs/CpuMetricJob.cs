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
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<CpuMetricJob> _logger;
        private readonly ICpuMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        public CpuMetricJob(ICpuMetricsRepository metricsRepository, IAgentsRepository agentsRepository, ILogger<CpuMetricJob> logger, ICpuMetricsAgentClient agentClient, IMapper mapper)
        {
            _metricsRepository = metricsRepository;
            _agentsRepository = agentsRepository;
            _logger = logger;
            _agentClient = agentClient;
            _mapper = mapper;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var enabledAgents = _agentsRepository.GetRegistered().Where(item => item.Enabled);
            foreach (var agent in enabledAgents)
            {
                SyncronizeMetricsFromAgent(agent);
            }
            return Task.CompletedTask;
        }

        private void SyncronizeMetricsFromAgent(AgentInfo agentInfo)
        {
            try
            {
                var lastTime = _metricsRepository.GetMetricsLastDateFormAgent(agentInfo.AgentId);
                var response = _agentClient.GetMetrics(new CpuMetricClientRequest
                {
                    BaseUrl = agentInfo.AgentUrl,
                    FromTime = lastTime,
                    ToTime = DateTimeOffset.Now
                });
                foreach (var clientMetric in response.Metrics)
                {
                    _metricsRepository.Create(_mapper.Map<CpuMetric>(clientMetric));
                }
                _logger.LogDebug($"Sincronized {response.Metrics.Count} CpuMetrics");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}