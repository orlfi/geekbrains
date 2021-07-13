using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsAgent.Features.Queries;
using MediatR;

namespace MetricsAgent.Controllers
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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsByPeriod([FromRoute] NetworkMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Parameters: {request}");
            
            var response = await _mediator.Send(request);
            
            return Ok(response);
        }
    }
}
