using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductSelectList
{
    internal sealed class ReadProductSelectListQueryHandler : IRequestHandler<ReadProductSelectListQuery, List<ProductSelectListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadProductSelectListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<ProductSelectListDto>> Handle(ReadProductSelectListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Product>()
                    .Include(x => x.ProductTypes)
                    .Where(x => !x.IsDeleted && x.ProductTypes.Any(pt => pt.Type == request.ProductType))
                    .ProjectTo<ProductSelectListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}
