using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private Storage _storage;
        public WeatherForecastController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = _storage.Data.Where(item => item.Date >= from && item.Date <= to).ToList();
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            if (temperature < -273)
                return BadRequest($"The temperature cannot be less than -273 in Celsius");

            if (_storage.Data.Exists(item => item.Date == date))
            {
                return Conflict($"The temperature for the date {date} has already been created");
            } else
            {
                _storage.Data.Add(new DateTemperature(date, temperature));
                return Ok();
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            if (temperature < -273)
                return BadRequest($"The temperature cannot be less than -273 in Celsius");

            var dateTemperature = _storage.Data.FirstOrDefault(item => item.Date == date);
            if (dateTemperature == null)
            {
                return BadRequest($"Update error! Temperature for date {date} not found");
            } else
            {
                dateTemperature.Temperature = temperature;
                return Ok();
            }

        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            _storage.Data.RemoveAll(item => item.Date >= from && item.Date <= to);
            return Ok();
        }
    }
}
