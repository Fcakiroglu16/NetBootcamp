using System.Reflection;
using Bootcamp.Repository.Categories;
using Bootcamp.Repository.Identities;
using Bootcamp.Repository.Products;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.Repository
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AppUser>().Property(x => x.Name).HasMaxLength(100);
            //modelBuilder.Entity<AppUser>().ToTable("CustomUserTableName");


            //   product barcode index
            //modelBuilder.Entity<Product>().HasIndex(x => x.Barcode);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}