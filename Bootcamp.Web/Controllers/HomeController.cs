using Bootcamp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using Bootcamp.Web.Signin;
using Bootcamp.Web.WeatherServices;
using Bootcamp.Web.TokenServices;
using Bootcamp.Web.Users;
using Bootcamp.Web.Users.Signin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bootcamp.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger,
        WeatherService weatherService,
        UserService userService,
        IDataProtectionProvider dataProtectionProvider) : Controller
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


        public async Task<IActionResult> Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninViewModel signinViewModel)
        {
            var result = await userService.Signin(signinViewModel);

            if (!result.IsSuccess)
            {
                result.Errors!.ForEach(error => { ModelState.AddModelError(string.Empty, error); });

                return View(signinViewModel);
            }


            return RedirectToAction(nameof(HomeController.Index));
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index));
        }


        [Authorize(Roles = "admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        [Authorize]
        public IActionResult X()
        {
            var dataProtector = dataProtectionProvider.CreateProtector("abc");

            // DES/3DES/AES =>

           

            var valueAsEncrypt = HttpContext.Request.Cookies["bgcolor"];

            var bgcolor = dataProtector.Unprotect(valueAsEncrypt);


            ViewBag.bgColor = bgcolor;

            TempData["bgcolor2"] = bgcolor;
            return RedirectToAction("Index", "Y");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
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