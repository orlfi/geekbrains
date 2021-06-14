using System;

namespace MetricsDAL.Models
{
    public class CpuMetric
    {
        int Id { get; set; }

        int Value { get; set; }

        TimeSpan Time { get; set; }
    }
}
