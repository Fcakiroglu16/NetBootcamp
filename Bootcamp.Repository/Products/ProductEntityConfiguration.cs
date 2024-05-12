using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bootcamp.Repository.Products
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.Property(product => product.Name).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Price).IsRequired().HasPrecision(18, 2);
            builder.Property(product => product.Created).IsRequired();
            builder.Property(product => product.Barcode).IsRequired().HasMaxLength(100);

            //builder.HasData(new Product()
            //{
            //    Id = 1,
            //    Price = 100,
            //    Name = "Product 1",
            //    Barcode = "123456789",
            //}, new Product()
            //{
            //    Id = 2,
            //    Price = 100,
            //    Name = "Product 2",
            //    Barcode = "123456789",
            //});
        }
    }
}