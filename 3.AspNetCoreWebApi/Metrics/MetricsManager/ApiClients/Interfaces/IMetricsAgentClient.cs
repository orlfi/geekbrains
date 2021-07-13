using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.ApiClients.Interfaces
{
    public interface IMetricsAgentClient<TRequest, TResponse> 
        where TRequest:class 
        where TResponse:class
    {
        TResponse GetMetrics(TRequest request);
    }
}
