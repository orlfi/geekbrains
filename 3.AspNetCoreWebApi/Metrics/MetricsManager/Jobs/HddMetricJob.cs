using MetricsManager.DAL;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricJob> _logger;

        public HddMetricJob(IHddMetricsRepository repository, ILogger<HddMetricJob> logger)
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