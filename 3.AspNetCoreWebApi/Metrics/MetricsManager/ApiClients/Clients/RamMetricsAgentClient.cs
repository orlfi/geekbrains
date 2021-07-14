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
    public class RamMetricsAgentClient : IRamMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RamMetricsAgentClient>  _logger;

        public RamMetricsAgentClient(HttpClient httpClient, ILogger<RamMetricsAgentClient> logger) => (_httpClient, _logger) = (httpClient, logger);

        public async Task<AgentRamMetricResponse> GetMetrics(RamMetricClientRequest request)
        {

            string url = $"{request.BaseUrl}api/metrics/ram/available/from/{request.FromTime:o}/to/{request.ToTime:o}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage);
                using var responseStream = await responseMessage.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<AgentRamMetricResponse>(responseStream);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when receiving agent data {request.BaseUrl}: {ex.Message}");
            }

            return null;
        }
    }
}