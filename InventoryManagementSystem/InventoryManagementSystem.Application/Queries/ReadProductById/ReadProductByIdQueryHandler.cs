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
            return await _context.Set<Product>().AsNoTracking()
                 .Where(x => !x.IsDeleted)
                 .Where(x => x.Id == request.Id)
                 .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                 .Select(productDto => new ProductDTO
                 {
                     Id = productDto.Id,
                     Amount = productDto.Amount,
                     Price = productDto.Price,
                     Name = productDto.Name,
                     Description = productDto.Description,
                     IsDeleted = productDto.IsDeleted,
                     Supplier = productDto.Supplier,
                     Document = productDto.Document,
                     ProductTypes = productDto.ProductTypes,
                     OrderLines = productDto.OrderLines,
                     Ingredients = productDto.Ingredients.Where(ingredient => !ingredient.IsDeleted).ToList()
                 })
                 .SingleAsync(cancellationToken);



        }
    }
}
