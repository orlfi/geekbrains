using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MetricsManager.Features.Queries.Agents;
using MetricsManager.Features.Commands.Agents;


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

        /// <summary>
        /// Returns information about all registered agents
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/agents
        ///
        /// </remarks>
        /// <returns>List of registered agents</returns>
        /// <response code="200">If everything is ok</response>
        [HttpGet]
        public async Task<IActionResult> GetRegisteredAgents()
        {
            _logger.LogDebug(1, "Getting registered Agents List");

            var response = await _mediator.Send(new GetRegisteredAgentsQuery());

            return Ok(response);
        }

        /// <summary>
        /// Registers a new agent
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/agents/register
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>информацию о зарегистрированном объекте</returns>
        /// <response code="200">If everything is ok</response>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAgent([FromBody] RegisterAgentCommand request)
        {
            _logger.LogInformation($"Register Agent Parameters: command={request}");

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Enables agent
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/enable/1
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpPut("enable/{agentId}")]
        public async Task<IActionResult> EnableAgentById([FromRoute] EnableAgentByIdCommand request)
        {
            _logger.LogInformation($"EnableAgentById Parameters: command={request}");

            await _mediator.Send(request);

            return Ok();
        }

        /// <summary>
        /// Disables agent
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/disable/1
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpPut("disable/{agentId}")]
        public async Task<IActionResult> DisableAgentById([FromRoute] DisableAgentByIdCommand request)
        {
            _logger.LogInformation($"DisableAgentById Parameters: command={request}");

            await _mediator.Send(request);

            return Ok();
        }
    }
}
