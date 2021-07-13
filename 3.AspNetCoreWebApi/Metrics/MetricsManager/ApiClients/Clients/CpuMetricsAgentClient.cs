using System;
using System.Net.Http;
using MetricsManager.ApiClients.Interfaces;
using MetricsManager.ApiClients.Requests;
using MetricsManager.Responses.Metrics;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MetricsManager.ApiClients.Clients
{
    public class CpuMetricsAgentClient : ICpuMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CpuMetricsAgentClient>  _logger;

        public CpuMetricsAgentClient(HttpClient httpClient, ILogger<CpuMetricsAgentClient> logger) => (_httpClient, _logger) = (httpClient, logger);

        public CpuMetricResponse GetMetrics(CpuMetricClientRequest request)
        {

            string url = $"{request.BaseUrl}api/metrics/cpu/from/{request.FromTime:o}/to/{request.ToTime:o}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var responseMessage = _httpClient.SendAsync(requestMessage).Result;
                string responseStream = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonSerializer.Deserialize<CpuMetricResponse>(responseStream);
                return null;
                // using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                // return JsonSerializer.DeserializeAsync<CpuMetricResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}