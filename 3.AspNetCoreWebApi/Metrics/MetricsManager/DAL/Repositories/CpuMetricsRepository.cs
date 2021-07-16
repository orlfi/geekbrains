using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;
        public CpuMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(CpuMetric item)
        {
            var connection = _connectionManager.GetOpenedConnection();
            connection.Execute("INSERT INTO CpuMetrics(AgentId, Value, Time) VALUES (@AgentId, @Value, @Time)",
                new
                {
                    item.AgentId,
                    item.Value,
                    item.Time
                });
        }

        public IList<CpuMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<CpuMetric>("SELECT Id, AgentId, Value, Time FROM CpuMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();
            
            return result;
        }

        public IList<CpuMetric> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<CpuMetric>("SELECT Id, AgentId, Value, Time FROM CpuMetrics WHERE Time >= @FromTime AND Time <= @ToTime AND AgentId = @AgentId",
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

            var result = connection.ExecuteScalar<DateTimeOffset>("SELECT Max(Time) FROM CpuMetrics WHERE AgentId = @AgentId",
                new
                {
                    AgentId = agentId
                });

            return result;
        }
    }
}
