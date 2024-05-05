namespace NetBootcamp.API.Products
{
    public interface IProductRepository
    {
        IReadOnlyList<Product> GetAll();

        void Update(Product product);
        void Create(Product product);

        Product? GetById(int id);

        void Delete(int id);
    }
}