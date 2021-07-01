using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System.Data.SQLite;


namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;
        public CpuMetricsRepository(IConnectionManager connectionManager) => _connectionManager =  connectionManager;

        public void Create(CpuMetric item)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"INSERT INTO CpuMetrics(Value, Time) VALUES ({item.Value}, {item.Time.ToUnixTimeSeconds()})";
            command.ExecuteNonQuery();
        }

        public IList<CpuMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"SELECT Id, Value, Time FROM CpuMetrics WHERE Time >= {fromTime.ToUnixTimeSeconds()} AND Time <= {toTime.ToUnixTimeSeconds()}";

            var result = new List<CpuMetric>();

            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    result.Add(new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)).ToOffset(TimeZoneInfo.Local.BaseUtcOffset)
                    });
                }
            }

            return result;
        }
    }
}
