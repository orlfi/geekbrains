using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class NetworkMetricResponse
    {
        public List<NetworkMetricDTO> Metrics { get; set; } = new List<NetworkMetricDTO>();
    }
}
