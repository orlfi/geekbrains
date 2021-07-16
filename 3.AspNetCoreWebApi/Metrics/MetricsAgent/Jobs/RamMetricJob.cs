using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;
        private readonly ILogger<RamMetricJob> _logger;

        public RamMetricJob(IRamMetricsRepository repository, ILogger<RamMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;

            _ramCounter = new PerformanceCounter("Memory", "available mbytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramAvailable = Convert.ToInt32(_ramCounter.NextValue());

            _repository.Create(new RamMetric
            {
                Time = DateTimeOffset.Now,
                Value = ramAvailable
            });
            
            _logger.LogDebug("Available {0} mbytes by {1}", ramAvailable, DateTimeOffset.Now);
            
            return Task.CompletedTask;
        }
    }
}