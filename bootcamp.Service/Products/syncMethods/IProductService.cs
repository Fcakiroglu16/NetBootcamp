using NetBootcamp.API.Products.ProductCreateUseCase;
using System.Collections.Immutable;
using bootcamp.Service.Products.DTOs;
using bootcamp.Service.Products.Helpers;
using bootcamp.Service.SharedDTOs;

namespace NetBootcamp.API.Products
{
    public interface IProductService
    {
        ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax(PriceCalculator priceCalculator);


        ResponseModelDto<ProductDto?> GetByIdWithCalculatedTax(int id, PriceCalculator priceCalculator);

        ResponseModelDto<ImmutableList<ProductDto>> GetAllByPageWithCalculatedTax(
            PriceCalculator priceCalculator, int page, int pageSize);


        ResponseModelDto<int> Create(ProductCreateRequestDto request);
        ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request);

        ResponseModelDto<NoContent> UpdateProductName(int id, string name);


        ResponseModelDto<NoContent> Delete(int id);
    }
}