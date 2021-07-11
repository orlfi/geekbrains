using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsAgent.DAL.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public RamMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(RamMetric item)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            connection.Execute("INSERT INTO RamMetrics(Value, Time) VALUES (@Value, @Time)",
                new
                {
                    item.Value,
                    Time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<RamMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            var result = connection.Query<RamMetric>("SELECT Id, Value, Time FROM RamMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();

            return result;
        }
    }
}
