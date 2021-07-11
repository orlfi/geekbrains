using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>, IClusterRepository<NetworkMetric>
    {
    }
}
