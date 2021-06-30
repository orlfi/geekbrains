using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class DotNetMetricResponse
    {
        public List<DotNetMetricDTO> Metrics { get; set; } = new List<DotNetMetricDTO>();
    }
}
