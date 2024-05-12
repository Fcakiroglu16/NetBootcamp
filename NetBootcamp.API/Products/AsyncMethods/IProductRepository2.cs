using NetBootcamp.API.Repositories;

namespace NetBootcamp.API.Products.AsyncMethods
{
    public interface IProductRepository2 : IGenericRepository<Product>
    {
        Task UpdateProductName(string name, int id);
    }
}