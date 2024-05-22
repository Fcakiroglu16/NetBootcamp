using Bootcamp.Domain.Products;

namespace Bootcamp.ApplicationService.Products
{
    public class ProductService(
        IProductRepository productRepository,
        ICacheService cacheService,
        IUnitOfWork unitOfWork)
    {
        public async Task<ResponseModelDto<int>> CreateProduct(ProductCreateRequestDto request)
        {
            unitOfWork.BeginTransaction();


            var productToCreate = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            // save to db;

            var product = await productRepository.CreateProduct(productToCreate);

            await unitOfWork.TransactionCommitAsync()!;

            return ResponseModelDto<int>.Success(product.Id);
        }

        public async Task<ResponseModelDto<ProductDto>> GetProductById(int id)
        {
            //cache aside design pattern
            var hasProductAsCache = cacheService.Get<ProductDto?>($"product:{id}");

            if (hasProductAsCache is not null)
            {
                return ResponseModelDto<ProductDto>.Success(hasProductAsCache);
            }

            var product = await productRepository.GetProductById(id);

            if (product is null)
            {
                return ResponseModelDto<ProductDto>.Fail("not found");
            }


            cacheService.Add($"product:{id}", new ProductDto(product.Id, product.Name, product.Price));


            return ResponseModelDto<ProductDto>.Success(new ProductDto(product.Id, product.Name, product.Price));
        }
    }
}