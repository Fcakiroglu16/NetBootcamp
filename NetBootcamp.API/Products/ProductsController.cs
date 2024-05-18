using bootcamp.Service.Products.AsyncMethods;
using bootcamp.Service.Products.DTOs;
using bootcamp.Service.Products.Helpers;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;
using NetBootcamp.API.ExceptionHandlers;
using NetBootcamp.API.Filters;
using NetBootcamp.API.Products.ProductCreateUseCase;

namespace NetBootcamp.API.Products
{
    public class ProductsController : CustomBaseController
    {
        //private readonly IProductService _productService = ProductServiceFactory.GetService();

        private readonly IProductService2 _productService;

        public ProductsController(IProductService2 productService)
        {
            _productService = productService;
        }


        //baseUrl/api/products
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] PriceCalculator priceCalculator)
        {
            //throw new CriticalException("db hatası");
            return Ok(await _productService.GetAllWithCalculatedTax(priceCalculator));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [MyResourceFilter]
        [MyActionFilter]
        [MyResultFilter]
        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetById(int productId, [FromServices] PriceCalculator priceCalculator)
        {
            return CreateActionResult(await _productService.GetByIdWithCalculatedTax(productId, priceCalculator));
        }


        [HttpGet("page/{page:int}/pagesize/{pageSize:max(50)}")]
        public async Task<IActionResult> GetAllByPage(int page, int pageSize,
            [FromServices] PriceCalculator priceCalculator)
        {
            return CreateActionResult(
                await _productService.GetAllByPageWithCalculatedTax(priceCalculator, page, pageSize));
        }


        // complex type => class,record,struct => request body as Json
        // simple type => int,string,decimal => query string by default / route data

        [SendSmsWhenExceptionFilter]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequestDto request)
        {
            //throw new Exception("db'ye gidemedi");
            var result = await _productService.Create(request);

            return CreateActionResult(result, nameof(GetById), new { productId = result.Data });
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpPut("UpdateProductName")]
        public async Task<IActionResult> UpdateProductName(ProductNameUpdateRequestDto request)
        {
            return CreateActionResult(await _productService.UpdateProductName(request.Id, request.Name));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        // PUT localhost/api/products/10
        [HttpPut("{productId:int}")]
        public async Task<IActionResult> Update(int productId, ProductUpdateRequestDto request)
        {
            return CreateActionResult(await _productService.Update(productId, request));
        }


        //// PUT api/products   
        //[HttpPut]
        //public IActionResult Update2(ProductUpdateRequestDto request)
        //{
        //    _productService.Update(request);

        //    return NoContent();
        //}


        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {
            return CreateActionResult(await _productService.Delete(productId));
        }
    }
}