using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public RamMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(RamMetric item)
        {
            var connection = _connectionManager.GetOpenedConnection();
            connection.Execute("INSERT INTO RamMetrics(AgentId, Value, Time) VALUES (@AgentId, @Value, @Time)",
                new
                {
                    item.AgentId,
                    item.Value,
                    item.Time
                });
        }

        public IList<RamMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<RamMetric>("SELECT Id, AgentId, Value, Time FROM RamMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();

            return result;
        }

        public IList<RamMetric> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<RamMetric>("SELECT Id, AgentId, Value, Time FROM RamMetrics WHERE Time >= @FromTime AND Time <= @ToTime AND AgentId = @AgentId",
                new
                {
                    AgentId = agentId,
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();

            return result;
        }

        public DateTimeOffset GetMetricsLastDateFormAgent(int agentId)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.ExecuteScalar<DateTimeOffset>("SELECT Max(Time) FROM RamMetrics WHERE AgentId = @AgentId",
                new
                {
                    AgentId = agentId
                });

            return result;
        }
    }
}
