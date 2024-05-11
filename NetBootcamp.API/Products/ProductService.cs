using System.Collections.Immutable;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Products.DTOs;
using NetBootcamp.API.Products.ProductCreateUseCase;
using NetBootcamp.API.Redis;
using StackExchange.Redis;

namespace NetBootcamp.API.Products
{
    public class ProductService(IProductRepository productRepository, RedisService redisService) : IProductService
    {
        //private readonly IProductRepository _productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;

        //}

        private const string ProductCacheKey = "products";
        private const string ProductCacheKeyAsList = "products-list";

        public ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax(
            PriceCalculator priceCalculator)
        {
            if (redisService.Database.KeyExists(ProductCacheKey))
            {
                var productListAsJsonFromCache = redisService.Database.StringGet(ProductCacheKey);

                var productListFromCache =
                    JsonSerializer.Deserialize<ImmutableList<ProductDto>>(productListAsJsonFromCache!);


                return ResponseModelDto<ImmutableList<ProductDto>>.Success(productListFromCache);
            }


            var productList = productRepository.GetAll().Select(product => new ProductDto(
                product.Id,
                product.Name,
                priceCalculator.CalculateKdv(product.Price, 1.20m),
                product.Created.ToShortDateString()
            )).ToImmutableList();


            var productListAsJson = JsonSerializer.Serialize(productList);
            redisService.Database.StringSet(ProductCacheKey, productListAsJson);


            productList.ForEach(product =>
            {
                redisService.Database.ListLeftPush($"{ProductCacheKeyAsList}:{product.Id}",
                    JsonSerializer.Serialize(product));
            });


            Dictionary<string, string> dictionary = new()
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };


            foreach (var item in dictionary)
            {
                var entry = new HashEntry(item.Key, item.Value);
                redisService.Database.HashSet("hash-key", [entry]);
            }


            //risService.Database.HashSet("hash-key", dictionary.Select(x => new HashEntry(x.Key, x.Value)).ToArray());


            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productList);
        }


        public ResponseModelDto<ImmutableList<ProductDto>> GetAllByPageWithCalculatedTax(
            PriceCalculator priceCalculator, int page, int pageSize)
        {
            var productList = productRepository.GetAllByPage(page, pageSize).Select(product => new ProductDto(
                product.Id,
                product.Name,
                priceCalculator.CalculateKdv(product.Price, 1.20m),
                product.Created.ToShortDateString()
            )).ToImmutableList();


            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productList);
        }


        public ResponseModelDto<ProductDto?> GetByIdWithCalculatedTax(int id,
            PriceCalculator priceCalculator)
        {
            var customKey = $"{ProductCacheKeyAsList}:{id}";

            if (redisService.Database.KeyExists(customKey))
            {
                var productAsJsonFromCache = redisService.Database.ListGetByIndex(customKey, 0);

                var productFromCache = JsonSerializer.Deserialize<ProductDto>(productAsJsonFromCache!);

                return ResponseModelDto<ProductDto?>.Success(productFromCache);
            }


            var hasProduct = productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<ProductDto?>.Fail("Ürün bulunamadı", HttpStatusCode.NotFound);
            }


            redisService.Database.ListLeftPush($"{ProductCacheKeyAsList}:{hasProduct.Id}",
                JsonSerializer.Serialize(hasProduct));


            var newDto = new ProductDto(
                hasProduct.Id,
                hasProduct.Name,
                priceCalculator.CalculateKdv(hasProduct.Price, 1.20m),
                hasProduct.Created.ToShortDateString()
            );

            return ResponseModelDto<ProductDto?>.Success(newDto);
        }

        // write Add Method
        public ResponseModelDto<int> Create(ProductCreateRequestDto request)
        {
            redisService.Database.KeyDelete(ProductCacheKey);


            // fast fail
            // Guard clauses

            //var hasProduct = productRepository.IsExists(request.Name.Trim());

            //if (hasProduct)
            //{
            //    return ResponseModelDto<int>.Fail("Oluşturma çalıştığınız ürün bulunmaktadır.",
            //        HttpStatusCode.BadRequest);
            //}


            var newProduct = new Product
            {
                Id = productRepository.GetAll().Count + 1,
                Name = request.Name.Trim(),
                Price = request.Price,
                Created = DateTime.Now
            };

            productRepository.Create(newProduct);

            return ResponseModelDto<int>.Success(newProduct.Id, HttpStatusCode.Created);
        }

        // write update method


        public ResponseModelDto<NoContent> UpdateProductName(int productId, string name)
        {
            redisService.Database.KeyDelete(ProductCacheKey);
            var hasProduct = productRepository.GetById(productId);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Güncellenmeye çalışılan ürün bulunamadı.",
                    HttpStatusCode.NotFound);
            }

            productRepository.UpdateProductName(name, productId);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request)
        {
            redisService.Database.KeyDelete(ProductCacheKey);
            var hasProduct = productRepository.GetById(productId);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Güncellenmeye çalışılan ürün bulunamadı.",
                    HttpStatusCode.NotFound);
            }

            var updatedProduct = new Product
            {
                Id = productId,
                Name = request.Name,
                Price = request.Price,
                Created = hasProduct.Created
            };

            productRepository.Update(updatedProduct);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Delete(int id)
        {
            redisService.Database.KeyDelete(ProductCacheKey);
            var hasProduct = productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Silinmeye çalışılan ürün bulunamadı.",
                    HttpStatusCode.NotFound);
            }


            productRepository.Delete(id);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }
    }
}