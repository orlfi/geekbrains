using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces.Repositories
{
    public interface IClusterMetricsRepository<T> where T : class
    {
        IList<T> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        DateTimeOffset GetMetricsLastDateFormAgent(int agentId);
    }
}
