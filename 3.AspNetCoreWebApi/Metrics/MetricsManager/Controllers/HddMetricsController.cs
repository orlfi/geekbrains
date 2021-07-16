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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMediator _mediator;

        public HddMetricsController(ILogger<HddMetricsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

            _logger.LogDebug(1, "Logger dependency injected to HddMetricsController");
        }

        /// <summary>
        /// Gets HDD metrics on a given time range
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/hdd/agent/1/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("agent/{agentId}/disk-time/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] HddMetricGetByPeriodFromAgentQuery request)
        {
            _logger.LogInformation($"Hdd GetMetricsFromAgent Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets HDD metrics on a given time range from all agents
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/hdd/cluster/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("cluster/disk-time/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] HddMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Hdd GetMetricsFromAllCluster Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
