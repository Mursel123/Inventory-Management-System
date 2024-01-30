using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Price;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Prices.ReadPriceList
{
    internal class ReadPriceListByIngredientIdQueryHandler : IRequestHandler<ReadPriceListByIngredientIdQuery, List<PriceListDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public ReadPriceListByIngredientIdQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<PriceListDto>> Handle(ReadPriceListByIngredientIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Price>()
                   .Where(x => x.Ingredient.Id == request.Id)
                   .ProjectTo<PriceListDto>(_mapper.ConfigurationProvider)
                   .OrderBy(x => x.Id)
                   .ToListAsync(cancellationToken);
        }
    }
}
