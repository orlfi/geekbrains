using MetricsManager.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricJob> _logger;

        public NetworkMetricJob(INetworkMetricsRepository repository, ILogger<NetworkMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}