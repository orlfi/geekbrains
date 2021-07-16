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
    public class CpuMetricsAgentClient : ICpuMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CpuMetricsAgentClient>  _logger;

        public CpuMetricsAgentClient(HttpClient httpClient, ILogger<CpuMetricsAgentClient> logger) => (_httpClient, _logger) = (httpClient, logger);

        public async Task<AgentCpuMetricResponse> GetMetrics(CpuMetricClientRequest request)
        {

            string url = $"{request.BaseUrl}api/metrics/cpu/from/{request.FromTime:o}/to/{request.ToTime:o}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage);
                using var responseStream = await responseMessage.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<AgentCpuMetricResponse>(responseStream);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when receiving agent data {request.BaseUrl}: {ex.Message}");
            }

            return null;
        }
    }
}