using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.ReadProductList;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.ReadProductById
{
    public class ReadProductByIdQueryHandler : IRequestHandler<ReadProductByIdQuery, ProductDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadProductByIdQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ProductDTO> Handle(ReadProductByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _context.Set<Product>().AsNoTracking()
                 .Where(x => !x.IsDeleted)
                 .Where(x => x.Id == request.Id)
                 .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                 .SingleOrDefaultAsync(cancellationToken);
            return test;


        }
    }
}
