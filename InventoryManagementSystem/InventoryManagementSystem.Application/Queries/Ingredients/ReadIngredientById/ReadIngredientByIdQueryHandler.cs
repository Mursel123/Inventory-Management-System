using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Application.Exceptions;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientById
{
    public class ReadIngredientByIdQueryHandler : IRequestHandler<ReadIngredientByIdQuery, IngredientDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadIngredientByIdQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IngredientDto> Handle(ReadIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            var ingredient =  await _context.Set<Ingredient>()
                 .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider)
                 .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (ingredient is null)
                throw new NotFoundException($"{nameof(Ingredient)} {request.Id} is not found.");

            return ingredient;
        }
    }
}
