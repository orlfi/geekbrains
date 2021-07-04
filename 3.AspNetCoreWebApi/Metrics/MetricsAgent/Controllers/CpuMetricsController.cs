using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Requests.Get;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper= mapper;

            _logger.LogDebug(1,"Logger dependency injected to CpuMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByPeriod([FromRoute] CpuMetricsGetByPeriodRequest request)
        {
            _logger.LogInformation($"Parameters: {request}");

            var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

            var response = new CpuMetricResponse();

            response.Metrics.AddRange(_mapper.Map<List<CpuMetricDto>>(metricsList));

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation($"Parameters: request={request}");

            if (request.Value < 0 || request.Value > 100)
                return BadRequest("The Value must be in the range from 0 to 100");

            _repository.Create(_mapper.Map<CpuMetric>(request));

            return Ok();
        }
    }
}
