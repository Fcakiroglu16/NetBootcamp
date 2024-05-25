using Bootcamp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Bootcamp.Web.WeatherServices;

namespace Bootcamp.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger, WeatherService weatherService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        public async Task<IActionResult> Index()
        {
            #region 1.yol

            //1.yol
            //var response = await weatherService.GetWeatherForecastWithCity("istanbul");

            //if (response.IsSuccess)
            //{
            //    ViewBag.temp = response.Data;
            //}
            //else
            //{
            //    ViewBag.temp = "s?cakl? bilgisi al?namad?.";
            //} 

            #endregion

            ViewBag.temp = await weatherService.GetWeatherForecastWithCityBetter("istanbul");


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}