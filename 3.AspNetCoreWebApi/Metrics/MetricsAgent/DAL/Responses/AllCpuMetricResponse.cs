using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DTO.Responses
{
    public class AllCpuMetricResponse
    {
        public List<CpuMetricDTO> Metrics { get; set; } = new List<CpuMetricDTO>();
    }
}
