using System.Collections.Immutable;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Products.DTOs;
using NetBootcamp.API.Products.ProductCreateUseCase;
using NetBootcamp.API.Repositories;

namespace NetBootcamp.API.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
    {
        //private readonly IProductRepository _productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;

        //}


        public ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax(
            PriceCalculator priceCalculator)
        {
            var productList = productRepository.GetAll().Select(product => new ProductDto(
                product.Id,
                product.Name,
                priceCalculator.CalculateKdv(product.Price, 1.20m),
                product.Created.ToShortDateString()
            )).ToImmutableList();


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
            var hasProduct = productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<ProductDto?>.Fail("Ürün bulunamadı", HttpStatusCode.NotFound);
            }


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
                //Id = productRepository.GetAll().Count + 1,
                Name = request.Name.Trim(),
                Price = request.Price,
                Stock = 10,
                Barcode = Guid.NewGuid().ToString(),
                Created = DateTime.Now
            };

            productRepository.Create(newProduct);

            unitOfWork.Commit();
            return ResponseModelDto<int>.Success(newProduct.Id, HttpStatusCode.Created);
        }

        // write update method


        public ResponseModelDto<NoContent> UpdateProductName(int productId, string name)
        {
            var hasProduct = productRepository.GetById(productId);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Güncellenmeye çalışılan ürün bulunamadı.",
                    HttpStatusCode.NotFound);
            }

            // productRepository.UpdateProductName(name, productId);
            unitOfWork.Commit();
            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request)
        {
            var hasProduct = productRepository.GetById(productId);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Güncellenmeye çalışılan ürün bulunamadı.",
                    HttpStatusCode.NotFound);
            }


            hasProduct.Name = request.Name;
            hasProduct.Price = request.Price;


            productRepository.Update(hasProduct);


            unitOfWork.Commit();
            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Delete(int id)
        {
            var hasProduct = productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Silinmeye çalışılan ürün bulunamadı.",
                    HttpStatusCode.NotFound);
            }


            productRepository.Delete(id);
            unitOfWork.Commit();
            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }
    }
}