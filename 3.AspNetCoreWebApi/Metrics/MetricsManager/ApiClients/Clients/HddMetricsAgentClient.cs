using System;
using System.Net.Http;
using System.Threading.Tasks;
using MetricsManager.ApiClients.Interfaces;
using MetricsManager.ApiClients.Requests;
using Core.Responses;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MetricsManager.ApiClients.Clients
{
    public class HddMetricsAgentClient : IHddMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HddMetricsAgentClient>  _logger;

        public HddMetricsAgentClient(HttpClient httpClient, ILogger<HddMetricsAgentClient> logger) => (_httpClient, _logger) = (httpClient, logger);

        public async Task<AgentHddMetricResponse> GetMetrics(HddMetricClientRequest request)
        {

            string url = $"{request.BaseUrl}api/metrics/hdd/disk-time/from/{request.FromTime:o}/to/{request.ToTime:o}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage);
                using var responseStream = await responseMessage.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<AgentHddMetricResponse>(responseStream);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when receiving agent data {request.BaseUrl}: {ex.Message}");
            }

            return null;
        }
    }
}