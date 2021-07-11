using System;
using System.Data.SQLite;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MetricsAgent.DAL
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IConfiguration _configuration;
        
        public ConnectionManager(IConfiguration configuration) => _configuration = configuration;
        
        public SQLiteConnection CreateOpenedConnection()
        {
            var connction = new SQLiteConnection(_configuration.GetConnectionString("SqlLiteMetricsDatabase"));
            connction.Open();
            return connction;                
        }
    }
}
