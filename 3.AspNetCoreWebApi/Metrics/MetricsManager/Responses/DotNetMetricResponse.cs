using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class DotNetMetricResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; } = new List<DotNetMetricDto>();
    }
}
