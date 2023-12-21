using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.UpdateMl
{
    public class UpdateIngredientMlCommandHandler : IRequestHandler<UpdateIngredientMlCommand, Ingredient>
    {
        private readonly IDbContext _context;
        public UpdateIngredientMlCommandHandler(IDbContext context)
        {
            _context = context;
        }
        public async Task<Ingredient> Handle(UpdateIngredientMlCommand request, CancellationToken cancellationToken)
        {

            request.Ingredient.MlTotal = request.IsIncrement ?
                    request.Ingredient.MlTotal + request.MlTotal :
                    request.Ingredient.MlTotal - request.MlTotal;


            /*await _context.Set<Ingredient>()
                .Where(x => x.Id == request.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.MlTotal, 
                    x => request.IsIncrement ? 
                    x.MlTotal + request.MlTotal : 
                    x.MlTotal - request.MlTotal),cancellationToken);*/

            return request.Ingredient;
        }
    }
}
