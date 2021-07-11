using Quartz;
using System;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private readonly ILogger<DotNetMetricJob> _logger;

        public DotNetMetricJob(IDotNetMetricsRepository repository, ILogger<DotNetMetricJob> logger)
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