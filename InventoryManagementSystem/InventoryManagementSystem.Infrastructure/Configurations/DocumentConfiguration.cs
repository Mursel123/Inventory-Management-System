using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired();

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Document)
                .HasForeignKey<Order>("DocumentId");

            builder.HasOne(x => x.Product)
                .WithOne(x => x.Document)
                .HasForeignKey<Product>("DocumentId");

            builder.HasOne(x => x.SubProduct)
                .WithOne(x => x.Document)
                .HasForeignKey<SubProduct>("DocumentId");
        }
    }
}
