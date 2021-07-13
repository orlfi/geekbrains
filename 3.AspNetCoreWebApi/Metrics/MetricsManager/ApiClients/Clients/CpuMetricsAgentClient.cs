using System;
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

        public AgentCpuMetricResponse GetMetrics(CpuMetricClientRequest request)
        {

            string url = $"{request.BaseUrl}api/metrics/cpu/from/{request.FromTime:o}/to/{request.ToTime:o}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var responseMessage = _httpClient.SendAsync(requestMessage).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AgentCpuMetricResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}