using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddMetric>, IClusterRepository<HddMetric>
    {
    }
}
