using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class RamMetricResponse
    {
        public List<RamMetricDTO> Metrics { get; set; } = new List<RamMetricDTO>();
    }
}
