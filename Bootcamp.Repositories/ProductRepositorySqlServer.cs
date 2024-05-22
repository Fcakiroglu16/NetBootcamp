using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootcamp.ApplicationService.Products;
using Bootcamp.Domain.Products;

namespace Bootcamp.Repositories
{
    public class ProductRepositorySqlServer(AppDbContext context) : IProductRepository
    {
        public async Task<Product> CreateProduct(Product product)
        {
            context.Products.Add(product);

            return product;
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}