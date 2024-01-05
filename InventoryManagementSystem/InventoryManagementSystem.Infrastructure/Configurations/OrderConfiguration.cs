using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.OrderNumber)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.TotalCost)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .HasMany(x => x.OrderLines)
                .WithOne(x => x.Order);

            builder
                .Property(x => x.Type)
                .IsRequired();
        }
    }
}
