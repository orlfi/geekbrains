using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Quartz;
using Quartz.Impl.Matchers;
using MetricsManager.Services;
using MetricsManager.Features.Extensions;

namespace MetricsManager.Features.Commands
{
    public class ResumeJobCommand : IRequest
    {
        public string JobName { get; set; }

        public class ResumeJobCommandHandler : IRequestHandler<ResumeJobCommand>
        {
            private readonly IScheduler _scheduler;

            public ResumeJobCommandHandler(QuartsHostedService scheduleService) => _scheduler = scheduleService.Scheduler;

            public async Task<Unit> Handle(ResumeJobCommand request, CancellationToken cancellationToken)
            {
                string jobKeyName = $"MetricsManager.Jobs.{request.JobName.FirstCharToUpper()}MetricJob" + request.JobName;
                var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
                var jobKey = jobKeys.SingleOrDefault(key => key.Name == jobKeyName);
                
                if (jobKey != null)
                    await _scheduler.ResumeJob(jobKey);

                return Unit.Value;
            }
        }
    }
}
