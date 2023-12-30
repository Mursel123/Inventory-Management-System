using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Configurations
{
    internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
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
