using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using MetricsAgent.Jobs;

namespace MetricsAgent.Services
{ 
    public class QuartsHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;

        private IScheduler _scheduler;

        private readonly IJobFactory _jobFactory;

        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartsHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobSchedules = jobSchedules;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            _scheduler.JobFactory = _jobFactory;

            foreach(var jobSchedule in _jobSchedules)
            {
                IJobDetail jobDetail = CreateJobDetail(jobSchedule);
                ITrigger jobTrigger = CreateJobTrigger(jobSchedule);
                await _scheduler.ScheduleJob(jobDetail, jobTrigger, cancellationToken);
            }
            await _scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler?.Shutdown(cancellationToken);
        }

        private IJobDetail CreateJobDetail(JobSchedule jobSchedule)
        {
            Type jobType = jobSchedule.JobType;
            return JobBuilder.Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private ITrigger CreateJobTrigger(JobSchedule jobSchedule)
        {
            return TriggerBuilder.Create()
                .WithIdentity($"{jobSchedule.JobType.FullName}.trigger")
                .WithCronSchedule(jobSchedule.CronExpression)
                .WithDescription(jobSchedule.CronExpression)
                .Build();
        }
    }
}