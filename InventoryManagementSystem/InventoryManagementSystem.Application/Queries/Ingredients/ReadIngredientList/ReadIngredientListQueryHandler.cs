using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList
{
    public class ReadIngredientSelectListQueryHandler : IRequestHandler<ReadIngredientListQuery, List<IngredientListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadIngredientSelectListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<IngredientListDto>> Handle(ReadIngredientListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Ingredient>().AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ProjectTo<IngredientListDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
