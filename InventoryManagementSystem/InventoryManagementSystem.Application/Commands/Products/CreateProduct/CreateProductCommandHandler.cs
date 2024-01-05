using AutoMapper;
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

namespace InventoryManagementSystem.Application.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            
            var product = _mapper.Map<Product>(request);

            var ingredients = await _context.Set<Ingredient>()
                .AsTracking()
                .Where(s => request.Ingredients.Select(rs => rs.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            var types = await _context.Set<ProductType>()
                .AsTracking()
                .Where(s => request.ProductTypes.Select(rs => rs.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            var subProducts = await _context.Set<SubProduct>()
                .AsTracking()
                .Where(s => request.Products.Select(rs => rs.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            Supplier supplier = null;
            if (request.Supplier != null)
            {
                supplier = await _context.Set<Supplier>()
                                .AsTracking()
                                .Where(s => s.Id == request.Supplier.Id)
                                .SingleOrDefaultAsync(cancellationToken);
            }
            



            product.Ingredients = ingredients;
            product.ProductTypes = types;
            product.Supplier = supplier;
            product.SubProducts = subProducts;
            
            await _context.Set<Product>().AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return product.Id;
        }
    }
}
