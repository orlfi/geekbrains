using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repositories
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public HddMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(HddMetric item)
        {
            var connection = _connectionManager.GetOpenedConnection();

            connection.Execute("INSERT INTO HddMetrics(Value, Time) VALUES (@Value, @Time)",
                new
                {
                    item.Value,
                    Time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<HddMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<HddMetric>("SELECT Id, Value, Time FROM HddMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();

            return result;
        }

        public IList<HddMetric> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<HddMetric>("SELECT Id, AgentId, Value, Time FROM HddMetrics WHERE Time >= @FromTime AND Time <= @ToTime AND AgentId = @AgentId",
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
