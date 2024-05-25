using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.Web.Controllers
{
    public class YController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}