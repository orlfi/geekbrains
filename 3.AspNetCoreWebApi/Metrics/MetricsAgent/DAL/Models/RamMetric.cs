using System;

namespace MetricsAgent.DAL.Models
{
    public class RamMetric
    {
        int Id { get; set; }

        int Value { get; set; }

        TimeSpan Time { get; set; }
    }
}
