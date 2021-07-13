using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Interfaces.Repositories;

namespace MetricsManager.DAL.Interfaces.Repositories
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>, IClusterMetricsRepository<CpuMetric>, IGetByPeriodRepository<CpuMetric>
    {
    }
}
