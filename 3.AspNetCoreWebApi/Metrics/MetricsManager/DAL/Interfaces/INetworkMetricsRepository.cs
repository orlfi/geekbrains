using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>, IClusterMetricsRepository<NetworkMetric>, IGetByPeriodRepository<NetworkMetric>
    {
    }
}
