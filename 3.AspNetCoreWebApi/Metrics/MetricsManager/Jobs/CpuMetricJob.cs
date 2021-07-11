using Quartz;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricJob> _logger;

        public CpuMetricJob(ICpuMetricsRepository repository, ILogger<CpuMetricJob> logger)
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