using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Responses
{
    public class AgentRamMetricResponse
    {
        public List<AgentRamMetricDto> Metrics { get; set; } = new List<AgentRamMetricDto>();
    }
}
