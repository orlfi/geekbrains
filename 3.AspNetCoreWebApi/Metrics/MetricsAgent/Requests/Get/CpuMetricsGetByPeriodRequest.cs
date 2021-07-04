using System;

namespace MetricsAgent.Requests.Get
{
    public class CpuMetricsGetByPeriodRequest
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }
    }
}
