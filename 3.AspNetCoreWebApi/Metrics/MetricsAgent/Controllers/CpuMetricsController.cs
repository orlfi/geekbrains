using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsAgent.Features.Queries;
using MetricsAgent.Features.Commands;
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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CpuMetricCreateCommand request)
        {
            _logger.LogInformation($"Parameters: request={request}");

            //TODO Add FluentValidation?
            if (request.Value < 0 || request.Value > 100)
                return BadRequest("The Value must be in the range from 0 to 100");

            await _mediator.Send(request);

            return Ok();
        }
    }
}
