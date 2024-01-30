using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductByType
{
    internal sealed class ReadProductListByTypeQueryHandler : IRequestHandler<ReadProductListByTypeQuery, List<ProductSelectListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadProductListByTypeQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<ProductSelectListDto>> Handle(ReadProductListByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Product>()
                    .Include(x => x.ProductTypes)
                    .Where(x => x.ProductTypes.Any(pt => pt.Type == request.ProductType))
                    .ProjectTo<ProductSelectListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}
