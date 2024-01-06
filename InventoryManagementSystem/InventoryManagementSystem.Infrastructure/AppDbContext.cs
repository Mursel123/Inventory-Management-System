using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Infrastructure
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Document>? Document { get; set; }
        public DbSet<Ingredient>? Ingredient { get; set; }
        public DbSet<Order>? Order { get; set; }
        public DbSet<OrderLine>? OrderLine { get; set; }
        public DbSet<Price>? Price { get; set; }
        public DbSet<Product>? Product { get; set; }
        public DbSet<ProductType>? ProductType { get; set; }
        public DbSet<Supplier>? Supplier { get; set; }
        public DbSet<Settings>? Settings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            base.OnModelCreating(builder);
        }

        

        async Task IDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync(cancellationToken);
        }
    }
}
