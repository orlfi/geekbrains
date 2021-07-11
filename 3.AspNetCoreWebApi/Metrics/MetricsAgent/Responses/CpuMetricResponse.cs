using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class CpuMetricResponse
    {
        public List<CpuMetricDto> Metrics { get; set; } = new List<CpuMetricDto>();
    }
}
