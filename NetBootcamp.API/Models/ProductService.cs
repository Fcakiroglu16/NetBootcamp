using System.Collections.Immutable;
using System.Net;
using NetBootcamp.API.DTOs;

namespace NetBootcamp.API.Models
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository = new();


        public ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax()
        {
            var productList = _productRepository.GetAll().Select(product => new ProductDto(
                product.Id,
                product.Name,
                CalculateKdv(product.Price, 1.20m),
                product.Created.ToShortDateString()
            )).ToImmutableList();


            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productList);
        }

        public ResponseModelDto<ProductDto?> GetByIdWithCalculatedTax(int id)
        {
            var hasProduct = _productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<ProductDto?>.Fail("Ürün bulunamadı", HttpStatusCode.NotFound);
            }


            var newDto = new ProductDto(
                hasProduct.Id,
                hasProduct.Name,
                CalculateKdv(hasProduct.Price, 1.20m),
                hasProduct.Created.ToShortDateString()
            );

            return ResponseModelDto<ProductDto?>.Success(newDto);
        }

        // write Add Method
        public ResponseModelDto<int> Create(ProductCreateRequestDto request)
        {
            var newProduct = new Product
            {
                Id = _productRepository.GetAll().Count + 1,
                Name = request.Name,
                Price = request.Price,
                Created = DateTime.Now
            };

            _productRepository.Create(newProduct);

            return ResponseModelDto<int>.Success(newProduct.Id, HttpStatusCode.Created);
        }

        // write update method

        public ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request)
        {
            var hasProduct = _productRepository.GetById(productId);

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

            _productRepository.Update(updatedProduct);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Delete(int id)
        {
            var hasProduct = _productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Silinmeye çalışılan ürün bulunamadı.",
                    HttpStatusCode.NotFound);
            }


            _productRepository.Delete(id);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        private decimal CalculateKdv(decimal price, decimal tax) => price * tax;
    }
}