using Bootcamp.ApplicationService.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        // product create
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateRequestDto request)
        {
            var response = await productService.CreateProduct(request);

            if (response.IsSuccess)
            {
                return Created(string.Empty, response);
            }

            return BadRequest(response.FailMessages);
        }

        //get productId
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await productService.GetProductById(id);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return NotFound(response.FailMessages);
        }
    }
}