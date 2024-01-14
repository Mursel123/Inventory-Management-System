using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Amount)
                .IsRequired();

            builder
                .Property(x => x.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(255);

            builder
                .HasOne(x => x.Supplier)
                .WithMany(x => x.Products);

            builder
                .HasMany(x => x.Ingredients)
                .WithMany(x => x.Products);

            builder
                .HasMany(p => p.SubProducts)
                .WithMany();

            builder
                .HasMany(x => x.ProductTypes)
                .WithMany(x => x.Products);

            builder
                .HasMany(x => x.OrderLines)
                .WithOne(x => x.Product);
        }
    }
}
