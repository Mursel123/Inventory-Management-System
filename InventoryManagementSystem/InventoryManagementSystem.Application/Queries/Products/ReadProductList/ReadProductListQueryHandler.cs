using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.StaticData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductList
{
    public class ReadProductListQueryHandler : IRequestHandler<ReadProductListQuery, IReadOnlyList<ProductListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadProductListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IReadOnlyList<ProductListDto>> Handle(ReadProductListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Product>()
            .Where(x => !x.IsDeleted)
            .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        }
    }
}
