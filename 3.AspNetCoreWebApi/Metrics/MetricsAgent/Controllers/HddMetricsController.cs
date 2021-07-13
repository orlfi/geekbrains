using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsAgent.Features.Queries;
using MediatR;

namespace MetricsAgent.Controllers
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

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsByPeriod([FromRoute] HddMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Parameters: {request}");

            var response = await _mediator.Send(request);
            
            return Ok(response);
        }
    }
}
