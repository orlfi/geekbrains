using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses.Metrics
{
    public class CpuMetricResponse
    {
        public List<CpuMetricDto> Metrics { get; set; } = new List<CpuMetricDto>();
    }
}
