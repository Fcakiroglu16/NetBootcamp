using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Models;

namespace NetBootcamp.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly ProductService _productService = new();

        //baseUrl/api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAllWithCalculatedTax());
        }

        [HttpGet("{productId}")]
        public IActionResult GetById(int productId)
        {
            return CreateActionResult(_productService.GetByIdWithCalculatedTax(productId));
        }


        // complex type => class,record,struct => request body as Json
        // simple type => int,string,decimal => query string by default / route data

        [HttpPost]
        public IActionResult Create(ProductCreateRequestDto request)
        {
            var result = _productService.Create(request);

            return CreateActionResult(result, nameof(GetById), new { productId = result.Data });
        }

        // PUT localhost/api/products/10
        [HttpPut("{productId}")]
        public IActionResult Update(int productId, ProductUpdateRequestDto request)
        {
            return CreateActionResult(_productService.Update(productId, request));
        }


        //// PUT api/products   
        //[HttpPut]
        //public IActionResult Update2(ProductUpdateRequestDto request)
        //{
        //    _productService.Update(request);

        //    return NoContent();
        //}

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            return CreateActionResult(_productService.Delete(productId));
        }
    }
}