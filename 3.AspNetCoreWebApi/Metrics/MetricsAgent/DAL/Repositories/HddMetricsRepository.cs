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
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly IConfiguration _configuration;
        public HddMetricsRepository(IConfiguration configuration) => _configuration = configuration;

        public void Create(HddMetric item)
        {
            var connectionString = _configuration.GetConnectionString("SqlLiteMetricsDatabase");
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"INSERT INTO HddMetrics(Value, Time) VALUES ({item.Value}, {item.Time.ToUnixTimeSeconds()})";
            command.ExecuteNonQuery();
        }

        public IList<HddMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connectionString = _configuration.GetConnectionString("SqlLiteMetricsDatabase");
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"SELECT Id, Value, Time FROM HddMetrics WHERE Time >= {fromTime.ToUnixTimeSeconds()} AND Time <= {toTime.ToUnixTimeSeconds()}";

            var result = new List<HddMetric>();

            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    result.Add(new HddMetric
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
