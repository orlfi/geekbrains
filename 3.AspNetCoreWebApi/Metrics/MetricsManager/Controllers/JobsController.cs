using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MetricsManager.Features.Queries.Jobs;
using MetricsManager.Features.Commands.Jobs;


namespace MetricsManager.Controllers
{
    [Route("api/Jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IMediator _mediator;

        public JobsController(ILogger<AgentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _logger.LogDebug(1, "Logger dependency injected to AgentsController");
        }

        /// <summary>
        /// Returns information about Jobs
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/jobs
        ///
        /// </remarks>
        /// <returns>List of Job names</returns>
        /// <response code="200">If everything is in order</response>
        [HttpGet]
        public async Task<IActionResult> GetJobNames()
        {
            _logger.LogDebug(1, "Getting Job Names");

            var response = await _mediator.Send(new GetJobsNamesQuery());

            return Ok(response);
        }

        /// <summary>
        /// Pause Jobs
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/jobs/pause
        ///     {
        ///         "JobName": "CpuMetricJob"
        ///     }
        /// </remarks>
        /// <param name="command">Job name. You can get a list of jobs: /api/jobs</param>
        /// <response code="200">If everything is in order</response>
        [HttpPut("pause")]
        public async Task<IActionResult> PauseJob([FromBody] PauseJobCommand command)
        {
            _logger.LogInformation($"Pause job Parameters: command={command}");

            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Resume Jobs
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/jobs/resume
        ///     {
        ///         "JobName": "CpuMetricJob"
        ///     }
        /// </remarks>
        /// <param name="command">Job name. You can get a list of jobs: /api/jobs</param>
        /// <response code="200">If everything is in order</response>
        [HttpPut("resume")]
        public async Task<IActionResult> ResumeJob([FromBody] ResumeJobCommand command)
        {
            _logger.LogInformation($"Resume job Parameters: command={command}");

            await _mediator.Send(command);

            return Ok();
        }
    }
}
