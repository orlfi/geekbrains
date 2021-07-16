using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses.Jobs
{
    public class JobNamesResponse
    {
        public List<JobNameDto> JobNames { get; set; } = new List<JobNameDto>();
    }
}
