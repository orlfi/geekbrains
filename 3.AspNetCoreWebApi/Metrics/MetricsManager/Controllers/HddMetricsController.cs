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

        [HttpGet("agent/{agentId}/disk-time/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] HddMetricGetByPeriodFromAgentQuery request)
        {
            _logger.LogInformation($"Hdd GetMetricsFromAgent Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("cluster/disk-time/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] HddMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Hdd GetMetricsFromAllCluster Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
