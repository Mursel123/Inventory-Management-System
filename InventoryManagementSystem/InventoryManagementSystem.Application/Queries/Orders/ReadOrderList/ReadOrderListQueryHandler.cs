using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Order;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Orders.ReadOrderList
{
    internal sealed class ReadOrderListQueryHandler : IRequestHandler<ReadOrderListQuery, IReadOnlyList<OrderListDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public ReadOrderListQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IReadOnlyList<OrderListDto>> Handle(ReadOrderListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Order>()
                    .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync(cancellationToken);
        }
    }
}
