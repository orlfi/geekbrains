using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public NetworkMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(NetworkMetric item)
        {
            var connection = _connectionManager.GetOpenedConnection();

            connection.Execute("INSERT INTO NetworkMetrics(Value, Time) VALUES (@Value, @Time)",
                new
                {
                    item.Value,
                    Time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<NetworkMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<NetworkMetric>("SELECT Id, Value, Time FROM NetworkMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();

            return result;
        }

        public IList<NetworkMetric> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<NetworkMetric>("SELECT Id, AgentId, Value, Time FROM NetworkMetrics WHERE Time >= @FromTime AND Time <= @ToTime AND AgentId = @AgentId",
                new
                {
                    AgentId = agentId,
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();

            return result;
        }
    }
}
