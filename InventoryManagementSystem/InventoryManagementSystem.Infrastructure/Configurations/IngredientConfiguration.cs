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
    internal sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.MlUsage)
                .HasPrecision(18,2)
                .IsRequired();

            builder
                .Property(x => x.MlTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .HasMany(x => x.Products)
                .WithMany(x => x.Ingredients);

            builder
                .HasMany(x => x.Prices)
                .WithOne(x => x.Ingredient);

            builder
                .HasMany(x => x.OrderLines)
                .WithOne(x => x.Ingredient);
        }
    }
}
