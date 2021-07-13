using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        List<System.Diagnostics.PerformanceCounter> _networkCounters;
        private readonly ILogger<NetworkMetricJob> _logger;

        public NetworkMetricJob(INetworkMetricsRepository repository, ILogger<NetworkMetricJob> logger)
        {
            _repository = repository;
            _logger = logger;

            var cards = GetNetworkCards();
            _networkCounters = new List<System.Diagnostics.PerformanceCounter>();
            foreach (var card in cards)
            {
                _networkCounters.Add(new System.Diagnostics.PerformanceCounter("Network Interface", "bytes sent/sec", card));
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            int sendByAllCards = 0;
            foreach (var counter in _networkCounters)
            {
                sendByAllCards += Convert.ToInt32(counter.NextValue());
            }

            _repository.Create(new NetworkMetric
            {
                Time = DateTimeOffset.Now,
                Value = sendByAllCards
            });

            _logger.LogDebug("Bytes sent in second {0}b/s % by {1}", sendByAllCards, DateTimeOffset.Now);

            return Task.CompletedTask;
        }

        private String[] GetNetworkCards()
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            return category.GetInstanceNames();
        }
    }
}