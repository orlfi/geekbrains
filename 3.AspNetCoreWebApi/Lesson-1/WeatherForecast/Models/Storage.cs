using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Models
{
    public class Storage
    {
        private const int initialCount = 5;

        public List<DateTemperature> Data { get; set; }

        public Storage()
        {
            Data = new List<DateTemperature>();
            Init();
        }

        private void Init()
        {
            Random rnd = new Random();
            int i = 0;
            while (i < initialCount)
            {
                DateTime dt = new DateTime(2021, 06, rnd.Next(1, 30), rnd.Next(0, 24), rnd.Next(0, 6) * 10, 00);

                if (!Data.Exists(item => item.Date == dt))
                    Data.Add(new DateTemperature(dt, rnd.Next(5, 25)));

                i++;
            }
        }
    }
}
