using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Responses
{
    public class AgentDotNetMetricResponse
    {
        public List<AgentDotNetMetricDto> Metrics { get; set; } = new List<AgentDotNetMetricDto>();
    }
}
