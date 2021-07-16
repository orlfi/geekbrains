using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL;
using Core.Interfaces;
using MediatR;
using MetricsAgent.Features.Mappers;
using MetricsAgent.Jobs;
using MetricsAgent.Services;
using Dapper;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using FluentMigrator.Runner;

namespace MetricsAgent
{
    public class Startup
    {
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

            services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton(_connectionManager);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddMapper();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(typeof(CpuMetricJob), "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(typeof(DotNetMetricJob), "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(typeof(HddMetricJob), "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(typeof(NetworkMetricJob), "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(typeof(RamMetricJob), "0/5 * * * * ?"));
            services.AddHostedService<QuartsHostedService>();

            ConfigureDapperMapperForDateTimeOffset();

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // добавляем поддержку SQLite 
                    .AddSQLite()
                    // устанавливаем строку подключения
                    .WithGlobalConnectionString(_connectionManager.ConnectionString)
                    // подсказываем где искать классы с миграциями
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
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

            migrationRunner.MigrateUp();
        }

        private void ConfigureDapperMapperForDateTimeOffset()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetMappingHandler());
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset?));
        }
    }
}
