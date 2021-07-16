using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Responses
{
    public class AgentCpuMetricResponse
    {
        public List<AgentCpuMetricDto> Metrics { get; set; } = new List<AgentCpuMetricDto>();
    }
}
