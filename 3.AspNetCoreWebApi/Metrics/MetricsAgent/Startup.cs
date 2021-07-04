using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL;
using Core.Interfaces;
using System.Data.SQLite;
using MediatR;
using MetricsAgent.Features.Mappers;
using Dapper;

namespace MetricsAgent
{
    public class Startup
    {
        private readonly string[] _tableNames = { "CpuMetrics", "DotNetMetrics", "RamMetrics", "NetworkMetrics", "HddMetrics" };

        private const int initRowCount = 10;
        public IConnectionManager _connectionManager;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _connectionManager = new ConnectionManager(Configuration);

            services.AddControllers();
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IConnectionManager>(_connectionManager);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddMapper();

            ConfigureDapperMapper();
            ConfigureSqlLiteConnection();
        }

        private void ConfigureDapperMapper()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset?));
        }

        private void ConfigureSqlLiteConnection()
        {
            using var connection = _connectionManager.CreateOpenedConnection();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using var command = new SQLiteCommand(connection);
            int month = 6;
            foreach (var item in _tableNames)
            {
                var initializeWithDataFlag = Configuration.GetValue<bool>("InitializeWithData");
                if (initializeWithDataFlag)
                {
                    command.CommandText = $"DROP TABLE IF EXISTS {item}";
                    command.ExecuteNonQuery();

                }

                command.CommandText = $"CREATE TABLE IF NOT EXISTS {item}(Id INTEGER PRIMARY KEY, Value INT, Time INTEGER)";
                command.ExecuteNonQuery();

                if (initializeWithDataFlag)
                {
                    InitializeTableWithData(item, month++, command);
                }
            }
        }

        private void InitializeTableWithData(string tableName, int month, SQLiteCommand command)
        {
            Random rnd = new Random();
            var time = new DateTimeOffset(2021, month, DateTimeOffset.Now.Date.Day, DateTimeOffset.Now.Hour, 0, 0, TimeSpan.FromHours(3));
            for (int i = 0; i < initRowCount; i++)
            {
                var value = rnd.Next(1, 10) * 10;
                command.CommandText = $"INSERT INTO {tableName}(Value, Time) VALUES ({value}, {time.ToUnixTimeSeconds()})";
                command.ExecuteNonQuery();
                time = time.AddMinutes(10);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
