﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Exceptions;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductById
{
    internal sealed class ReadProductByIdQueryHandler : IRequestHandler<ReadProductByIdQuery, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public ReadProductByIdQueryHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ProductDto> Handle(ReadProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product =  await _context.Set<Product>()
                 .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                 .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product is null)
                throw new NotFoundException($"{nameof(Product)} {request.Id} is not found.");

            return product;


        }
    }
}
