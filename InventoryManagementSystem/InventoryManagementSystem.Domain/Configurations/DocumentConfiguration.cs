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
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Order).WithOne(x => x.Document).HasForeignKey<Order>("DocumentId");
            builder.HasOne(x => x.Product)
                .WithOne(x => x.Document)
                .HasForeignKey<Product>("DocumentId");
        }
    }
}
