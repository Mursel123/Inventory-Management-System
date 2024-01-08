using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagementSystem.Application.Queries.ProductTypeList
{
    public class ReadProductTypeListQueryHandler : IRequestHandler<ReadProductTypeListQuery, List<ProductTypeDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public ReadProductTypeListQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductTypeDto>> Handle(ReadProductTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<ProductType>()
                            .Where(x => !x.IsDeleted)
                            .ProjectTo<ProductTypeDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);


        }
    }
}
