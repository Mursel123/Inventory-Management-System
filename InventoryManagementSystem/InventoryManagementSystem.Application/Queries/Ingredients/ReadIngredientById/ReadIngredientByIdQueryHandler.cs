using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientById
{
    public class ReadIngredientByIdQueryHandler : IRequestHandler<ReadIngredientByIdQuery, IngredientDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadIngredientByIdQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IngredientDTO> Handle(ReadIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Ingredient>()
                 .AsNoTracking()
                 .ProjectTo<IngredientDTO>(_mapper.ConfigurationProvider)
                 .SingleAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
