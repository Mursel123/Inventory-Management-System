using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Application.Exceptions;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Delete
{
    internal sealed class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Guid>
    {
        private readonly IDbContext _context;
        public DeleteIngredientCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var deletedRows = await _context.Set<Ingredient>()
                .Where(x => x.Id == request.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.IsDeleted, true), cancellationToken);

            if (deletedRows is not 1)
                throw new NotFoundException($"{nameof(Ingredient)} {request.Id} is not found.");

            return request.Id;
        }
    }
}
