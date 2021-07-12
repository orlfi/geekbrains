using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Interfaces;

namespace MetricsManager.DAL.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>, IClusterMetricsRepository<CpuMetric>, IGetByPeriodRepository<CpuMetric>
    {
    }
}
