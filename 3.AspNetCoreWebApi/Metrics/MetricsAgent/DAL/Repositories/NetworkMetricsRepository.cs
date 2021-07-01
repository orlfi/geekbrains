using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System.Data.SQLite;


namespace MetricsAgent.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public NetworkMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(NetworkMetric item)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"INSERT INTO NetworkMetrics(Value, Time) VALUES ({item.Value}, {item.Time.ToUnixTimeSeconds()})";
            command.ExecuteNonQuery();
        }

        public IList<NetworkMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connectionManager.CreateOpenedConnection();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"SELECT Id, Value, Time FROM NetworkMetrics WHERE Time >= {fromTime.ToUnixTimeSeconds()} AND Time <= {toTime.ToUnixTimeSeconds()}";

            var result = new List<NetworkMetric>();

            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    result.Add(new NetworkMetric
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
