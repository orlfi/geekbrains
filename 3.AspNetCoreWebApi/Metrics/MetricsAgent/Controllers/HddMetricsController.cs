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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;

            _logger.LogDebug(1, "Logger dependency injected to HddMetricsController");
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByPeriod([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Parameters: fromTime={fromTime} toTime={toTime}");

            var metricsList = _repository.GetByPeriod(fromTime, toTime);

            var response = new HddMetricResponse();

            foreach (var item in metricsList)
            {

                response.Metrics.Add(new HddMetricDto
                {
                    Id = item.Id,
                    Value = item.Value,
                    Time = item.Time
                });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation($"Parameters: request={request}");

            if (request.Value < 0 || request.Value > 100)
                return BadRequest("The Value must be in the range from 0 to 100");

            HddMetric metric = new HddMetric()
            {
                Value = request.Value,
                Time = request.Time
            };

            _repository.Create(metric);

            return Ok();
        }
    }
}
