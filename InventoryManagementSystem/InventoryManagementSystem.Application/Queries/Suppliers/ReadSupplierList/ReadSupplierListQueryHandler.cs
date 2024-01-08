using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierList
{
    public class ReadSupplierListQueryHandler : IRequestHandler<ReadSupplierListQuery, List<SupplierDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public ReadSupplierListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SupplierDto>> Handle(ReadSupplierListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Supplier>()
                    .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Id)
                    .ToListAsync(cancellationToken);
        }
    }
}
