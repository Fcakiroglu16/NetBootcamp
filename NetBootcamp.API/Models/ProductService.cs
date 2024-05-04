using System.Collections.Immutable;
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

        private decimal CalculateKdv(decimal price, decimal tax) => price * tax;


        public ProductDto? GetById(int id)
        {
            var hasProduct = _productRepository.GetById(id);

            if (hasProduct is null)
            {
                return null!;
            }

            return new ProductDto(
                hasProduct.Id,
                hasProduct.Name,
                CalculateKdv(hasProduct.Price, 1.20m),
                hasProduct.Created.ToShortDateString()
            );
        }

        public ResponseModelDto<NoContent> Delete(int id)
        {
            var hasProduct = _productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Silinmeye çalışılan ürün bulunamadı.");
            }


            _productRepository.Delete(id);

            return ResponseModelDto<NoContent>.Success();
        }
    }
}