using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.ApiClients.Requests;
using MetricsManager.Responses.Metrics;

namespace MetricsManager.ApiClients.Interfaces
{
    public interface ICpuMetricsAgentClient: IMetricsAgentClient<CpuMetricClientRequest, CpuMetricResponse>
    {
    }
}
