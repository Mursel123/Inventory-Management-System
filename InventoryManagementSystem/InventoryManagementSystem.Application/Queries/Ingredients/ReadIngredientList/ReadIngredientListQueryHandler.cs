using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList
{
    public class ReadIngredientSelectListQueryHandler : IRequestHandler<ReadIngredientListQuery, IReadOnlyList<IngredientListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadIngredientSelectListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IReadOnlyList<IngredientListDto>> Handle(ReadIngredientListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Ingredient>().AsNoTracking()
                .ProjectTo<IngredientListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
