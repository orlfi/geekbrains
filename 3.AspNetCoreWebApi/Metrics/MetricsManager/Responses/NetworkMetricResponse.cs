using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class NetworkMetricResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; } = new List<NetworkMetricDto>();
    }
}
