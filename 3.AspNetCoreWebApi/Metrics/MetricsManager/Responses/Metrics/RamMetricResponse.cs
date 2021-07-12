using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses.Metrics
{
    public class RamMetricResponse
    {
        public List<RamMetricDto> Metrics { get; set; } = new List<RamMetricDto>();
    }
}
