using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        
        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;

            _logger.LogDebug(1,"Logger dependency injected to CpuMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByPeriod([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Parameters: fromTime={fromTime} toTime={toTime}");

            var metricsList = _repository.GetByPeriod(fromTime, toTime);

            var response = new CpuMetricResponse();

            foreach (var item in metricsList)
            {

                response.Metrics.Add(new CpuMetricDto
                {
                    Id = item.Id,
                    Value = item.Value,
                    Time = item.Time
                });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation($"Parameters: request={request}");

            if (request.Value < 0 || request.Value > 100)
                return BadRequest("The Value must be in the range from 0 to 100");

            CpuMetric metric = new CpuMetric()
            {
                Value = request.Value,
                Time = request.Time
            };

            _repository.Create(metric);

            return Ok();
        }
    }
}
