using System.Linq.Expressions;
using LoggingAndMiddleware.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingAndMiddleware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ILogger<ProductsController> logger, ILoggerFactory loggerFactory) : ControllerBase
    {
        //get method
        [HttpGet]
        public IActionResult Get()
        {
            var customILogger = loggerFactory.CreateLogger("CustomLogger");

            customILogger.LogInformation("This is a custom log message");
            logger.LogInformation("This is a log message");
            // ILogger
            // Trace
            // Debug
            // Info
            // Warning
            // Error
            // Critical


            return Ok("Get all products");
        }

        //post method
        [HttpPost]
        public IActionResult Post()
        {
            var response = new HttpClient().GetAsync("htts://www.google.com").Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ExceptionSaveToDatabase("Error while creating product");
            }


            return Ok("product created");
        }
    }
}