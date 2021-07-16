using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MetricsManager.DAL.Repositories;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL;
using Core.Interfaces;
using MediatR;
using MetricsManager.Features.Mappers;
using MetricsManager.Jobs;
using MetricsManager.Services;
using Dapper;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using FluentMigrator.Runner;
using MetricsManager.DAL.DapperMapingHandlers;
using MetricsManager.ApiClients.Interfaces;
using MetricsManager.ApiClients.Clients;
using Polly;
using Microsoft.OpenApi.Models;

namespace MetricsManager
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
            services.AddSingleton<IAgentsRepository, AgentsRepository>();
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
            services.AddSingleton<QuartsHostedService>();
            services.AddHostedService<QuartsHostedService>(provider => provider.GetService<QuartsHostedService>());

            services.AddHttpClient<ICpuMetricsAgentClient, CpuMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            services.AddHttpClient<IDotNetMetricsAgentClient, DotNetMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            services.AddHttpClient<IHddMetricsAgentClient, HddMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            services.AddHttpClient<INetworkMetricsAgentClient, NetworkMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            services.AddHttpClient<IRamMetricsAgentClient, RamMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Metric collection agent service API",
                    Description = @"Provides collection data
                        Loading processor, Free Memory
                        Using hard disk, Return speed over the network
                        Heap size  from registered agents",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Orlikov Fedor",
                        Email = "orlfi@mail.ru",
                        Url = new Uri("https://orlfi.tk"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
            });

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // adding SQLite support
                    .AddSQLite()
                    // setting the connection string
                    .WithGlobalConnectionString(_connectionManager.ConnectionString)
                    // we suggest where to look for classes with migrations
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            ConfigureDapperMappers();
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса агента сбора метрик");
                c.RoutePrefix = string.Empty;
            });

            migrationRunner.MigrateUp();
        }

        private void ConfigureDapperMappers()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetMappingHandler());
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset?));

            SqlMapper.AddTypeHandler(new UriMappingHandler());
            SqlMapper.RemoveTypeMap(typeof(Uri));
        }
    }
}
