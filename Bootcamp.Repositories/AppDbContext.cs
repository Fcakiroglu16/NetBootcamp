using Bootcamp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}