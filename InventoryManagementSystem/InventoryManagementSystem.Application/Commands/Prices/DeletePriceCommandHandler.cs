using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Application.Exceptions;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Commands.Prices
{
    public class DeletePriceCommandHandler : IRequestHandler<DeletePriceCommand, Guid>
    {
        private readonly IDbContext _context;

        public DeletePriceCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeletePriceCommand request, CancellationToken cancellationToken)
        {
            var deletedRows = await _context.Set<Price>()
                .Where(x => x.Id == request.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.IsDeleted, true), cancellationToken);

            if (deletedRows is not 1)
                throw new NotFoundException($"{nameof(Price)} {request.Id} is not found.");

            return request.Id;
        }
    }
}
