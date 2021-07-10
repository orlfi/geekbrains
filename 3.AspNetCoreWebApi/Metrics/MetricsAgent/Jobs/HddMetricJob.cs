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
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;
        private readonly ILogger<HddMetricJob> _logger;

        public HddMetricJob(IHddMetricsRepository repository, ILogger<HddMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;

            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var diskTime = Convert.ToInt32(_hddCounter.NextValue());

            _repository.Create(new HddMetric
            {
                Time = DateTimeOffset.Now,
                Value = diskTime
            });

            _logger.LogDebug("Disk Time {0}% by {1}", diskTime, DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}