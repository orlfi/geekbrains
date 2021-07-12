using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IGetByPeriodRepository<T> where T : class
    {
        IList<T> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
