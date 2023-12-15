using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Delete
{
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Guid>
    {
        private readonly IDbContext _context;
        public DeleteIngredientCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _context.Set<Ingredient>().SingleAsync(x => x.Id == request.Id, cancellationToken);

            ingredient.IsDeleted = true;

            _context.Set<Ingredient>().Update(ingredient);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
