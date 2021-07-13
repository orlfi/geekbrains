using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.ApiClients.Requests;
using Core.Responses;

namespace MetricsManager.ApiClients.Interfaces
{
    public interface ICpuMetricsAgentClient: IMetricsAgentClient<CpuMetricClientRequest, AgentCpuMetricResponse>
    {
    }
}
