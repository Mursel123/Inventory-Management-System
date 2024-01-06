using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientSelectList
{
    public class ReadIngredientSelectListQueryHandler : IRequestHandler<ReadIngredientSelectListQuery, List<IngredientSelectListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadIngredientSelectListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<IngredientSelectListDto>> Handle(ReadIngredientSelectListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Ingredient>()
                .Where(x => !x.IsDeleted)
                .ProjectTo<IngredientSelectListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
