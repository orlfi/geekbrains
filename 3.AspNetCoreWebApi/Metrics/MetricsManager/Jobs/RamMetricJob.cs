using Quartz;
using System;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricJob> _logger;

        public RamMetricJob(IRamMetricsRepository repository, ILogger<RamMetricJob> logger)
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