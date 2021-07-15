using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MetricsManager.Features.Queries.Agents;
using MetricsManager.Features.Commands;


namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IMediator _mediator;

        public AgentsController(ILogger<AgentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _logger.LogDebug(1, "Logger dependency injected to AgentsController");
        }

        [HttpGet]
        public async Task<IActionResult> GetRegisteredAgents()
        {
            _logger.LogDebug(1, "Getting registered Agents List");

            var response = await _mediator.Send(new GetRegisteredAgentsQuery());

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAgent([FromBody] RegisterAgentCommand command)
        {
            _logger.LogInformation($"Register Agent Parameters: command={command}");

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public async Task<IActionResult> EnableAgentById([FromRoute] EnableAgentByIdCommand command)
        {
            _logger.LogInformation($"EnableAgentById Parameters: command={command}");

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public async Task<IActionResult> DisableAgentById([FromRoute] DisableAgentByIdCommand command)
        {
            _logger.LogInformation($"DisableAgentById Parameters: command={command}");

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("pause/{jobName}")]
        public async Task<IActionResult> PauseJob([FromRoute] PauseJobCommand command)
        {
            _logger.LogInformation($"Pause job Parameters: command={command}");

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("resume/{jobName}")]
        public async Task<IActionResult> ResumeJob([FromRoute] ResumeJobCommand command)
        {
            _logger.LogInformation($"Resume job Parameters: command={command}");

            await _mediator.Send(command);

            return Ok();
        }
    }
}
