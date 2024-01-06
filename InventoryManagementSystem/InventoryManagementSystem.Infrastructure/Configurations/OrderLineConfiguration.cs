using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Quantity)
                .IsRequired();

            builder
                .Property(x => x.TotalCost)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderLines);

            builder
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.OrderLines);

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.OrderLines);

        }
    }
}
