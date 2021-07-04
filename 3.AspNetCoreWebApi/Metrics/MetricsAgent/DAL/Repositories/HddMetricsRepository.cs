using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsAgent.DAL.Repositories
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public HddMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(HddMetric item)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            connection.Execute("INSERT INTO HddMetrics(Value, Time) VALUES (@Value, @Time)",
                new
                {
                    item.Value,
                    Time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<HddMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            var result = connection.Query<HddMetric>("SELECT Id, Value, Time FROM HddMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();

            return result;
        }
    }
}
