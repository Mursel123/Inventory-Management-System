using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IDbContext _context;

        public DeleteProductCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Set<Product>().SingleAsync(x=> x.Id == request.Id, cancellationToken);

            product.IsDeleted = true;

            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
