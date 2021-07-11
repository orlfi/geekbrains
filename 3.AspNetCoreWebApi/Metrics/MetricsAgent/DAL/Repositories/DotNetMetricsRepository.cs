using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsAgent.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public DotNetMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(DotNetMetric item)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            connection.Execute("INSERT INTO DotNetMetrics(Value, Time) VALUES (@Value, @Time)",
                new
                {
                    item.Value,
                    item.Time
                });
        }

        public IList<DotNetMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            var result = connection.Query<DotNetMetric>("SELECT Id, Value, Time FROM DotNetMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();

            return result;
        }
    }
}
