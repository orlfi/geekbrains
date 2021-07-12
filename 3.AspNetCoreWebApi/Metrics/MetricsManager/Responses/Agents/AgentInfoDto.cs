using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses.Agents
{
    public class AgentInfoDto
    {
        public int AgentId { get; set; }
        public Uri AgentAddress { get; set; }
    }
}
