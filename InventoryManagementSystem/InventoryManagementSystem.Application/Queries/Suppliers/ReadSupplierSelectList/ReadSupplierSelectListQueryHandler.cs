using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierSelectList
{
    public class ReadSupplierSelectListQueryHandler : IRequestHandler<ReadSupplierSelectListQuery, List<SupplierSelectListDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public ReadSupplierSelectListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SupplierSelectListDto>> Handle(ReadSupplierSelectListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Supplier>()
                    .ProjectTo<SupplierSelectListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Id)
                    .ToListAsync(cancellationToken);
        }
    }
}
