using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList
{
    public class ReadIngredientListQueryHandler : IRequestHandler<ReadIngredientListQuery, List<IngredientListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadIngredientListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<IngredientListDTO>> Handle(ReadIngredientListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Ingredient>().AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ProjectTo<IngredientListDTO>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
