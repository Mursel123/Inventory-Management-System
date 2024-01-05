using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class SubProductConfiguration : IEntityTypeConfiguration<SubProduct>
    {
        public void Configure(EntityTypeBuilder<SubProduct> builder)
        {
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
                .WithMany(x => x.SubProducts);

            builder
                .HasMany(x => x.Products)
                .WithMany(x => x.SubProducts);

            builder
                .HasMany(x => x.OrderLines)
                .WithOne(x => x.SubProduct);
        }
    }
}
