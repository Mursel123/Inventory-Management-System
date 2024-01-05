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
    internal sealed class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.IngredientPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(x => x.Ml)
                .IsRequired();

            builder
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.Prices);
        }
    }
}
