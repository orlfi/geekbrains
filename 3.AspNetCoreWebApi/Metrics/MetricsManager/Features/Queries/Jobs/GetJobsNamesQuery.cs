using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Quartz;
using Quartz.Impl.Matchers;
using MetricsManager.Services;
using MetricsManager.Responses.Agents;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.Responses.Jobs;

using AutoMapper;

namespace MetricsManager.Features.Queries.Jobs
{
    public class GetJobsNamesQuery : IRequest<JobNamesResponse>
    {
        public class GetJobsNamesQueryHandler : IRequestHandler<GetJobsNamesQuery, JobNamesResponse>
        {
            private readonly IScheduler _scheduler;

            public GetJobsNamesQueryHandler(QuartsHostedService scheduleService) => _scheduler = scheduleService.Scheduler;

            public async Task<JobNamesResponse> Handle(GetJobsNamesQuery request, CancellationToken cancellationToken)
            {
                var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
                var response = new JobNamesResponse();
                foreach(var key in jobKeys)
                {
                    var jobDetail =  await _scheduler.GetJobDetail(key);
                    //var state = await _scheduler.GetTriggerState((key, GroupMatcher<TriggerKey>.AnyGroup());
                    var triggers = await _scheduler.GetTriggersOfJob(key);
                    var state = await _scheduler.GetTriggerState(triggers.First().Key);
                    response.JobNames.Add(new JobNameDto
                    {
                        JobName = key.Name[(key.Name.LastIndexOf('.') + 1)..],
                        State = state.ToString()

                    });
                }

                return  response;
            }
        }
    }
}
