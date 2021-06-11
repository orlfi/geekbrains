using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private ILogger<CpuMetricsController> _logger;
        
        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1,"Logger dependency injected to CpuMetricsController");
            _logger.LogInformation(1,"Из конструктора");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"Parameters: fromTime={fromTime} toTime={toTime}");
            _logger.LogError("Error - ошибка");
            _logger.LogCritical("Critical - критическая");
            _logger.LogWarning("Warning - ворнинг");
            _logger.LogTrace("Trace - трейс");
            _logger.LogDebug(1,"Дебаг из метода");
            return Ok(new { From = fromTime, To = toTime});
        }
    }
}
