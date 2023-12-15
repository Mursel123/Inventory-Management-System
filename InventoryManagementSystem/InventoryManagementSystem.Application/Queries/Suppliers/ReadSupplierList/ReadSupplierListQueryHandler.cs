using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierSelectList
{
    public class ReadSupplierListQueryHandler : IRequestHandler<ReadSupplierListQuery, List<SupplierDTO>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public ReadSupplierListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SupplierDTO>> Handle(ReadSupplierListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Supplier>()
                    .ProjectTo<SupplierDTO>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Id)
                    .ToListAsync(cancellationToken);
        }
    }
}
