using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsAgent.Features.Queries;
using MediatR;

namespace MetricsAgent.Controllers
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

            _logger.LogDebug(1,"Logger dependency injected to CpuMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsByPeriod([FromRoute] CpuMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
