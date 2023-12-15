using AutoMapper;
using InventoryManagementSystem.Application.Commands.Products.UpdateProduct;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Update
{
    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public UpdateIngredientCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _context.Set<Ingredient>()
                .AsTracking()
                .Include(x => x.Prices)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            _mapper.Map(request, ingredient, typeof(UpdateIngredientCommand), typeof(Ingredient));

            var prices = await _context.Set<Price>()
                            .AsTracking()
                            .Where(s => request.Prices.Select(rs => rs.Id).Contains(s.Id))
                            .ToListAsync(cancellationToken);

            ingredient.Prices = ingredient.Prices.Where(x => x.Id == Guid.Empty).ToList();

            ingredient.Prices.AddRange(prices);

            _context.Set<Ingredient>().Update(ingredient);
            await _context.SaveChangesAsync(cancellationToken);

            return ingredient.Id;
        }
    }
}
