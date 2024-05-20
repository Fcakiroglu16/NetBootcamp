namespace Bootcamp.Repository.Products
{
    public interface IProductRepository2 : IGenericRepository<Product, int>
    {
        Task UpdateProductName(string name, int id);
    }
}