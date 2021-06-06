using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Models
{
    public class DateTemperature
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }

        public DateTemperature(DateTime date, int temperature)
        {
            Date = date;
            Temperature = temperature;
        }
    }
}
