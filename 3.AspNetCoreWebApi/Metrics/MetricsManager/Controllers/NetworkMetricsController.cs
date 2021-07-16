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

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] NetworkMetricGetByPeriodFromAgentQuery request)
        {
            _logger.LogInformation($"Network GetMetricsFromAgent Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] NetworkMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Network GetMetricsFromAllCluster Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
