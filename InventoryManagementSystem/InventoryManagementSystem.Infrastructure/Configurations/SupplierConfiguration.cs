using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(x => x.Phone)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany(x => x.Products)
                .WithOne(x => x.Supplier);
        }
    }
}
