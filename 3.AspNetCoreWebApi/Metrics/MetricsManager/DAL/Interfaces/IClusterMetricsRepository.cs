using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IClusterMetricsRepository<T> where T : class
    {
        IList<T> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
