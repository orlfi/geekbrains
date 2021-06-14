using System;

namespace MetricsAgent.DAL.Models
{
    public class DotNetMetric
    {
        int Id { get; set; }

        int Value { get; set; }

        TimeSpan Time { get; set; }
    }
}
