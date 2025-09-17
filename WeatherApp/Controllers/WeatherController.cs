using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        public List<CityWeather> CityWeatherList { get; set; } = new List<CityWeather>()
        {
            new CityWeather() { CityUniqueCode = "NYC", CityName = "New York", DateAndTime = DateTime.Now, TemperatureFahrenheit = 32 },
            new CityWeather() { CityUniqueCode = "LAX", CityName = "Los Angeles", DateAndTime = DateTime.Now, TemperatureFahrenheit = 85 },
            new CityWeather() { CityUniqueCode = "CHI", CityName = "Chicago", DateAndTime = DateTime.Now, TemperatureFahrenheit = 50 },
            new CityWeather() { CityUniqueCode = "HOU", CityName = "Houston", DateAndTime = DateTime.Now, TemperatureFahrenheit = 90 },
            new CityWeather() { CityUniqueCode = "PHX", CityName = "Phoenix", DateAndTime = DateTime.Now, TemperatureFahrenheit = 100 }
        };

        [Route("/")]
        public IActionResult Index()
        {
            return View(CityWeatherList);
        }

        [Route("/{cityCode:alpha}")]
        public IActionResult Details(string cityCode)
        {
            CityWeather cityWeather = CityWeatherList.FirstOrDefault(cw => cw.CityUniqueCode.Equals(cityCode, StringComparison.OrdinalIgnoreCase));
            if (cityWeather == null)
            {
                return NotFound("City not found");
            }
            return View(cityWeather);
        }
    }
}
