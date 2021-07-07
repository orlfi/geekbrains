using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
namespace MetricsAgent.Schedule
{
    public class QuartsHostedService : IHostedService
    {
        ISchedulerFactory _schedulerFactory;

        IScheduler _scheduler;

        IJobFactory _jobFactory;

        IEnumerable<JobSchedule> _jobSchedules;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            _scheduler.JobFactory = _jobFactory;

            foreach(var jobSchedule in _jobSchedules)
            {
                

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}