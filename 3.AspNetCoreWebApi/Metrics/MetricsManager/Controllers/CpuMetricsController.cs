using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsManager.Features.Queries.Metrics;
using MediatR;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMediator _mediator;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

            _logger.LogDebug(1, "Logger dependency injected to CpuMetricsController");
        }
        
        /// <summary>
        /// Gets CPU metrics on a given time range
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/cpu/agent/1/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] CpuMetricGetByPeriodFromAgentQuery request)
        {
            _logger.LogInformation($"CPU GetMetricsFromAgent Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets CPU metrics on a given time range from all agents
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/cpu/cluster/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] CpuMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"CPU GetMetricsFromAllCluster Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
