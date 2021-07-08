using System;
using System.Data.SQLite;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MetricsAgent.DAL
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IConfiguration _configuration;
        
        public string ConnectionString => _configuration.GetConnectionString("SqlLiteMetricsDatabase");
        public ConnectionManager(IConfiguration configuration) => _configuration = configuration;
                
        public SQLiteConnection CreateOpenedConnection()
        {
            var connection = new SQLiteConnection(_configuration.GetConnectionString("SqlLiteMetricsDatabase"));
            connection.Open();
            return connection;                
        }
    }
}
