using AutoMapper;
using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.StaticData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateProductCommandHandler(IDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator; 
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Set<Product>()
                .AsTracking()
                .Include(x => x.Supplier)
                .Include(x => x.Ingredients)
                .Include(x => x.ProductTypes)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            _mapper.Map(request, product, typeof(UpdateProductCommand), typeof(Product));

            var ingredients = await _context.Set<Ingredient>()
                .AsTracking()
                .Where(s => request.Ingredients.Select(rs => rs.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            var types = await _context.Set<ProductType>()
                .AsTracking()
                .Where(s => request.ProductTypes.Select(rs => rs.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            Supplier? supplier = product.Supplier;
            if (supplier != null)
            {
                supplier = await _context.Set<Supplier>()
                                .AsTracking()
                                .Where(s => s.Id == request.Supplier.Id)
                                .SingleOrDefaultAsync(cancellationToken);
            }

            product.Ingredients = ingredients;
            product.Supplier = supplier;
            product.ProductTypes = types;
            CheckObjects(request, product);

            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            
            return product.Id;
        }

        private void CheckObjects(UpdateProductCommand request, Product product)
        {
            if (!request.ProductTypes.Exists(x => x.Type == ProductTypeData.SalesInventory))
            {
                product.Ingredients = new();
                product.Document = null;

            }
            if (!request.ProductTypes.Exists(x => x.Type == ProductTypeData.PurchasedInventory))
            {
                product.Supplier = null;
            }
        }

       
    }
}
