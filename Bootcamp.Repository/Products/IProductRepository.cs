namespace Bootcamp.Repository.Products
{
    public interface IProductRepository
    {
        IReadOnlyList<Product> GetAll();

        IReadOnlyList<Product> GetAllByPage(int page, int pageSize);

        void Update(Product product);


        void Create(Product product);

        Product? GetById(int id);

        void Delete(int id);

        bool IsExists(string productName);
    }
}