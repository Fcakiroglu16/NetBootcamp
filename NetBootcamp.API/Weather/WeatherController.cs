using bootcamp.Service.Weather;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;

namespace NetBootcamp.API.Weather
{
    public class WeatherController(IWeatherService weatherService) : CustomBaseController
    {
        [HttpGet]
        public IActionResult GetWeather(string city)
        {
            var weather = weatherService.GetWeather(city);

            return Ok(weather);
        }
    }
}