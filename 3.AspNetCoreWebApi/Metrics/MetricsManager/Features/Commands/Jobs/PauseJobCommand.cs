using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Quartz;
using Quartz.Impl.Matchers;
using MetricsManager.Services;
using MetricsManager.Features.Extensions;

namespace MetricsManager.Features.Commands.Jobs
{
    public class PauseJobCommand : IRequest
    {
        public string JobName { get; set; }

        public class PauseJobCommandHandler : IRequestHandler<PauseJobCommand>
        {
            private readonly IScheduler _scheduler;

            public PauseJobCommandHandler(QuartsHostedService scheduleService) => _scheduler = scheduleService.Scheduler;

            public async Task<Unit> Handle(PauseJobCommand request, CancellationToken cancellationToken)
            {
                string jobKeyName = $"MetricsManager.Jobs.{request.JobName}";
                var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
                var jobKey = jobKeys.SingleOrDefault(key => key.Name == jobKeyName);
                
                if (jobKey != null)
                    await _scheduler.PauseJob(jobKey);

                return Unit.Value;
            }
        }
    }
}
