using System;
using System.Threading.Tasks;
using System.Net.Http;
using MetricsManager.ApiClients.Interfaces;
using MetricsManager.ApiClients.Requests;
using Core.Responses;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MetricsManager.ApiClients.Clients
{
    public class DotNetMetricsAgentClient : IDotNetMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DotNetMetricsAgentClient>  _logger;

        public DotNetMetricsAgentClient(HttpClient httpClient, ILogger<DotNetMetricsAgentClient> logger) => (_httpClient, _logger) = (httpClient, logger);

        public async Task<AgentDotNetMetricResponse> GetMetrics(DotNetMetricClientRequest request)
        {

            string url = $"{request.BaseUrl}api/metrics/dotnet/heap-size/from/{request.FromTime:o}/to/{request.ToTime:o}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage);
                using var responseStream = await responseMessage.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<AgentDotNetMetricResponse>(responseStream);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when receiving agent data {request.BaseUrl}: {ex.Message}");
            }

            return null;
        }
    }
}