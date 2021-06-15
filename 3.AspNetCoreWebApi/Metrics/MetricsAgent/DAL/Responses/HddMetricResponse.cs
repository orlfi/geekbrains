using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class HddMetricResponse
    {
        public List<HddMetricDTO> Metrics { get; set; } = new List<HddMetricDTO>();
    }
}
