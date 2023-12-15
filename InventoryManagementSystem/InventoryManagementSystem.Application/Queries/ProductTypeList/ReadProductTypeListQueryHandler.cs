using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.ProductTypeList
{
    public class ReadProductTypeListQueryHandler : IRequestHandler<ReadProductTypeListQuery, List<ProductTypeDTO>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public ReadProductTypeListQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductTypeDTO>> Handle(ReadProductTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<ProductType>()
                            .Where(x => !x.IsDeleted)
                            .ProjectTo<ProductTypeDTO>(_mapper.ConfigurationProvider)
                            .OrderBy(x => x.Id)
                            .ToListAsync(cancellationToken);


        }
    }
}
