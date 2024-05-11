using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetBootcamp.API.Categories
{
    public class CategoryNameConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(category => category.Id);
            builder.Property(category => category.Id).ValueGeneratedNever();
            builder.Property(category => category.Name).IsRequired().HasMaxLength(100);
        }
    }
}