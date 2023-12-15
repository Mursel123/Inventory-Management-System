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

namespace InventoryManagementSystem.Application.Queries.ReadProductList
{
    public class ReadProductListQueryHandler : IRequestHandler<ReadProductListQuery, List<ProductListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadProductListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<ProductListDTO>> Handle(ReadProductListQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ProductType))
            {
                return await _context.Set<Product>()
                .Where(x => !x.IsDeleted)
                .ProjectTo<ProductListDTO>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
            }

            return await _context.Set<Product>()
            .Where(x => !x.IsDeleted && x.ProductTypes.Any(pt => pt.Type == request.ProductType))
            .ProjectTo<ProductListDTO>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);



        }
    }
}
