namespace Bootcamp.Repository.Products
{
    public class ProductRepository2 : GenericRepository<Product>, IProductRepository2
    {
        public ProductRepository2(AppDbContext context) : base(context)
        {
        }


        public async Task UpdateProductName(string name, int id)
        {
            var product = await GetById(id);
            product!.Name = name;
            await Update(product);
        }
    }
}