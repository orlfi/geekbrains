using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses.Metrics
{
    public class HddMetricResponse
    {
        public List<HddMetricDto> Metrics { get; set; } = new List<HddMetricDto>();
    }
}
