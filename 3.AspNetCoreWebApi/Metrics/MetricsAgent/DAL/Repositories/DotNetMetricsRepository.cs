using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System.Data.SQLite;


namespace MetricsAgent.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public DotNetMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(DotNetMetric item)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"INSERT INTO DotNetMetrics(Value, Time) VALUES ({item.Value}, {item.Time.ToUnixTimeSeconds()})";
            command.ExecuteNonQuery();
        }

        public IList<DotNetMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"SELECT Id, Value, Time FROM DotNetMetrics WHERE Time >= {fromTime.ToUnixTimeSeconds()} AND Time <= {toTime.ToUnixTimeSeconds()}";

            var result = new List<DotNetMetric>();

            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    result.Add(new DotNetMetric
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
