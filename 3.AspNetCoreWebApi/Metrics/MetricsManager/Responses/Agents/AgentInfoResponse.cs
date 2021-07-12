using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses.Agents
{
    public class AgentInfoResponse
    {
        public List<AgentInfoDto> Agents { get; set; } = new List<AgentInfoDto>();
    }
}
