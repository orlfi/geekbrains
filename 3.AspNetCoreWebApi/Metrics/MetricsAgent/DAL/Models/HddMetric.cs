using System;

namespace MetricsAgent.DAL.Models
{
    public class HddMetric
    {
        int Id { get; set; }

        int Value { get; set; }

        TimeSpan Time { get; set; }
    }
}
