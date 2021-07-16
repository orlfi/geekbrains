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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMediator _mediator;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

            _logger.LogDebug(1, "Logger dependency injected to NetworkMetricsController");
        }

        /// <summary>
        /// Gets Network metrics on a given time range
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/network/agent/1/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] NetworkMetricGetByPeriodFromAgentQuery request)
        {
            _logger.LogInformation($"Network GetMetricsFromAgent Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets Network metrics on a given time range from all agents
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/network/cluster/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] NetworkMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Network GetMetricsFromAllCluster Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
