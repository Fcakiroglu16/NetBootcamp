namespace NetBootcamp.API.Products
{
    public interface IProductRepository
    {
        IReadOnlyList<Product> GetAll();

        IReadOnlyList<Product> GetAllByPage(int page, int pageSize);

        void Update(Product product);

        void UpdateProductName(string name, int id);
        void Create(Product product);

        Product? GetById(int id);

        void Delete(int id);
    }
}