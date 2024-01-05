using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder
                .Property(x => x.Type)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany(x => x.Products)
                .WithMany(x => x.ProductTypes);

            builder.HasData(
                 new ProductType
                 {
                     Id = Guid.NewGuid(),
                     Type = ProductTypeData.PurchasedInventory
                 },
                 new ProductType
                 {
                     Id = Guid.NewGuid(),
                     Type = ProductTypeData.SalesInventory
                 });
        }
    }
}
