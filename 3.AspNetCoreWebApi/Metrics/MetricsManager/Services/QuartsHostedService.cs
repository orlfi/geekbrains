using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using MetricsManager.Jobs;

namespace MetricsManager.Services
{ 
    public class QuartsHostedService : IHostedService
    {
        ISchedulerFactory _schedulerFactory;

        public IScheduler Scheduler {get;set;}

        IJobFactory _jobFactory;

        IEnumerable<JobSchedule> _jobSchedules;

        public QuartsHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobSchedules = jobSchedules;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach(var jobSchedule in _jobSchedules)
            {
                IJobDetail jobDetail = CreateJobDetail(jobSchedule);
                ITrigger jobTrigger = CreateJobTrigger(jobSchedule);
                await Scheduler.ScheduleJob(jobDetail, jobTrigger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
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