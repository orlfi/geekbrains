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
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;
        private PerformanceCounter _cpuCounter;
        private readonly ILogger<CpuMetricJob> _logger;

        public CpuMetricJob(ICpuMetricsRepository repository, ILogger<CpuMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;

            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());

            _repository.Create(new CpuMetric
            {
                Time = DateTimeOffset.Now,
                Value = cpuUsageInPercents
            });

            _logger.LogDebug("Processor Time {0} % by {1}", cpuUsageInPercents, DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}