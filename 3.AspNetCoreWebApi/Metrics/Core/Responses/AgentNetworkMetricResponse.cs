using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Responses
{
    public class AgentNetworkMetricResponse
    {
        public List<AgentNetworkMetricDto> Metrics { get; set; } = new List<AgentNetworkMetricDto>();
    }
}
