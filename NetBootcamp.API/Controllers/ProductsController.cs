using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Models;

namespace NetBootcamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService = new();


        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetAllWithCalculatedTax());
        }

        //[HttpGet]
        //public IActionResult Get(int id)
        //{
        //    var product = _productService.GetById(id);

        //    if (product is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(product);
        //}


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.FailMessages);
            }

            return NoContent();
        }
    }
}