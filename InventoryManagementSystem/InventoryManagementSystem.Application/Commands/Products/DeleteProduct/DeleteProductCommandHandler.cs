using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Application.Exceptions;
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
            var deletedRows = await _context.Set<Product>()
                .Where(x => x.Id == request.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.IsDeleted, true), cancellationToken);

            if (deletedRows is not 1)
                throw new NotFoundException($"{nameof(Product)} {request.Id} is not found.");

            return request.Id;
        }
    }
}
