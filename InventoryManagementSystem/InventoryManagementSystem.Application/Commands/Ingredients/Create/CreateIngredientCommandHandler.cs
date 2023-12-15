using AutoMapper;
using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Create
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public CreateIngredientCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {

            var ingredient = _mapper.Map<Ingredient>(request);

            await _context.Set<Ingredient>().AddAsync(ingredient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ingredient.Id;
        }
    }
}
