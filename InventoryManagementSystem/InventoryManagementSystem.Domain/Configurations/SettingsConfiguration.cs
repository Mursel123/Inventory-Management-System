using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Configurations
{
    internal class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
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
