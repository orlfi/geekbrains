using System.Threading.Tasks;

namespace MetricsManager.ApiClients.Interfaces
{
    public interface IMetricsAgentClient<TRequest, TResponse> 
        where TRequest:class 
        where TResponse:class
    {
        Task<TResponse> GetMetrics(TRequest request);
    }
}
