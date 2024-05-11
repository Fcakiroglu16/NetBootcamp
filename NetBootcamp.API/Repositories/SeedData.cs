using NetBootcamp.API.Categories;
using NetBootcamp.API.Products;

namespace NetBootcamp.API.Repositories
{
    public class SeedData
    {
        public static void SeedDatabase(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;
            }

            var categories = new[]
            {
                new Category { Id = Guid.NewGuid(), Name = "Electronics" },
                new Category { Id = Guid.NewGuid(), Name = "Clothing" },
                new Category { Id = Guid.NewGuid(), Name = "Grocery" },
                new Category { Id = Guid.NewGuid(), Name = "Books" },
                new Category { Id = Guid.NewGuid(), Name = "Furniture" }
            };

            context.Categories.AddRange(categories);


            context.SaveChanges();
        }
    }
}