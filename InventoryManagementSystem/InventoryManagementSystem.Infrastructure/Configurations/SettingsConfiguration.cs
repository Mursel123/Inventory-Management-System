using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.AtLeastProductAmount)
                .IsRequired();

            builder
                .Property(x => x.AtLeastIngredientMLTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasData(
                new Settings()
                {
                    Id = Guid.NewGuid(),
                    AtLeastIngredientMLTotal = 0,
                    AtLeastProductAmount = 0,
                });
        }
    }
}
