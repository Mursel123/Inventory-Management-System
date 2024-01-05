using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Dashboard.ReadIngredientProductAlertList
{
    internal class ReadIngredientProductAlertListQueryHandler : IRequestHandler<ReadIngredientProductAlertListQuery, (List<ProductListDto>, List<IngredientListDTO>)>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public ReadIngredientProductAlertListQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<(List<ProductListDto>, List<IngredientListDTO>)> Handle(ReadIngredientProductAlertListQuery request, CancellationToken cancellationToken)
        {
            var settings = await _context.Set<Domain.Entities.Settings>().SingleAsync();

            var products = await _context.Set<Product>()
                .Where(x => x.Amount <= settings.AtLeastProductAmount && !x.IsDeleted)
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                
                .ToListAsync();

            var ingredients = await _context.Set<Ingredient>()
                .ProjectTo<IngredientListDTO>(_mapper.ConfigurationProvider)
                .Where(x => x.MlTotal <= settings.AtLeastIngredientMLTotal && !x.IsDeleted)
                .ToListAsync();

            return (products, ingredients);
        }
    }
}
