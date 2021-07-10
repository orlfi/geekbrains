using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;
        public CpuMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(CpuMetric item)
        {
            var connection = _connectionManager.GetOpenedConnection();
            connection.Execute("INSERT INTO CpuMetrics(Value, Time) VALUES (@Value, @Time)",
                new
                {
                    item.Value,
                    item.Time
                });
        }

        public IList<CpuMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<CpuMetric>("SELECT Id, Value, Time FROM CpuMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();
            
            return result;
        }
    }
}
