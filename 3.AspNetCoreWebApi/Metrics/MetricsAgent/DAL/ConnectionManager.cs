using System;
using System.Data.SQLite;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MetricsAgent.DAL
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IConfiguration _configuration;
        SQLiteConnection _connection = null;
        private readonly object locker = new object();

        public string ConnectionString => _configuration.GetConnectionString("SqlLiteMetricsDatabase");
        public ConnectionManager(IConfiguration configuration) => _configuration = configuration;
                
        public SQLiteConnection GetOpenedConnection()
        {
            lock (locker)
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection(_configuration.GetConnectionString("SqlLiteMetricsDatabase"));
                    _connection.Open();
                }
            }
            return _connection;                
        }
    }
}
