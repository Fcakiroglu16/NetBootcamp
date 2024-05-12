using NetBootcamp.API.Repositories;

namespace NetBootcamp.API.Products.AsyncMethods
{
    public class ProductRepositoryWithSqlServer(AppDbContext context) : IProductRepository
    {
        //private readonly AppDbContext _context;

        //public ProductRepositoryWithSqlServer(AppDbContext context)
        //{
        //    _context = context;
        //}

        public IReadOnlyList<Product> GetAll()
        {
            return context.Products.ToList().AsReadOnly();
        }

        public IReadOnlyList<Product> GetAllByPage(int page, int pageSize)
        {
            return context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList().AsReadOnly();
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
        }

        public void UpdateProductName(string name, int id)
        {
            var product = GetById(id);
            product!.Name = name;
            context.Products.Update(product);
        }

        public void Create(Product product)
        {
            context.Products.Add(product);
        }

        public Product? GetById(int id)
        {
            return context.Products.Find(id);
            //return context.Products.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            context.Products.Remove(product!);
        }

        public bool IsExists(string productName)
        {
            return context.Products.Any(x => x.Name == productName);
        }
    }
}