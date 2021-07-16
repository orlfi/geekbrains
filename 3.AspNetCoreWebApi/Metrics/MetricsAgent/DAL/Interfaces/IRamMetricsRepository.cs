﻿using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetric>, IGetByPeriodRepository<RamMetric>
    {
    }
}
