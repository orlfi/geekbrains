using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL.Models;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRegisteredAgents()
        {
            return Ok(new { Info = "Registered Agents List" });
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody]AgentInfo agentInfo)
        {
            return Ok(new { AgentInfo = agentInfo });
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok(new { AgentId = agentId });
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok(new { AgentId = agentId });
        }
    }
}
