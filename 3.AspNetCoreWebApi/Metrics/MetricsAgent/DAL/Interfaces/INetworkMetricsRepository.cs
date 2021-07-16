﻿using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>, IGetByPeriodRepository<NetworkMetric>
    {
    }
}
