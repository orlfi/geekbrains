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
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotNetCounter;
        private readonly ILogger<DotNetMetricJob> _logger;

        public DotNetMetricJob(IDotNetMetricsRepository repository, ILogger<DotNetMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;

            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "gen 1 heap size", "_global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var heapSize = Convert.ToInt32(_dotNetCounter.NextValue())/1024/1024;

            _repository.Create(new DotNetMetric
            {
                Time = DateTimeOffset.Now,
                Value = heapSize
            });

            _logger.LogDebug("CLR Memory gen 1 heap size {0} Мб by {1}", heapSize, DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}