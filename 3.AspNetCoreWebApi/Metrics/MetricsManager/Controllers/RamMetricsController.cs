using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsManager.Features.Queries.Metrics;
using MediatR;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMediator _mediator;

        public RamMetricsController(ILogger<RamMetricsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

            _logger.LogDebug(1, "Logger dependency injected to RamMetricsController");
        }

        /// <summary>
        /// Gets RAM metrics on a given time range
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/ram/agent/1/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("agent/{agentId}/available/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] RamMetricGetByPeriodFromAgentQuery request)
        {
            _logger.LogInformation($"Ram GetMetricsFromAgent Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets RAM metrics on a given time range from all agents
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/ram/cluster/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("cluster/available/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] RamMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Ram GetMetricsFromAllCluster Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
