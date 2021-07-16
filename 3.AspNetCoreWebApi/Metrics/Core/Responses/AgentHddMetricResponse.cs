using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Responses
{
    public class AgentHddMetricResponse
    {
        public List<AgentHddMetricDto> Metrics { get; set; } = new List<AgentHddMetricDto>();
    }
}
