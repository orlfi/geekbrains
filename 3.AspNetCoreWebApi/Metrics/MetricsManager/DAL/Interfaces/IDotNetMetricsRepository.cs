using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces.Repositories
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>, IClusterMetricsRepository<DotNetMetric>, IGetByPeriodRepository<DotNetMetric>
    {
    }
}
