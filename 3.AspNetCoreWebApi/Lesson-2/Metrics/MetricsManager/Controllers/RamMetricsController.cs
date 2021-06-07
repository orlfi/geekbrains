using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        [HttpGet("agent/{agentId}/from/{from}/to/{to}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTime from, [FromRoute] DateTime to)
        {
            return Ok();
        }

        [HttpGet("cluster/from/{from}/to/{to}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTime from, DateTime to)
        {
            return Ok();
        }
    }
}
