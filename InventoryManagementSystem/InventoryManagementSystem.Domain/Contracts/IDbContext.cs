using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Domain.Contracts
{
    public interface IDbContext
    {

        Task SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
