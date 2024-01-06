using AutoMapper;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                .Where(s => request.Ingredients.Contains(s.Id))
                .ToListAsync(cancellationToken);

            var types = await _context.Set<ProductType>()
                .AsTracking()
                .Where(s => request.ProductTypes.Contains(s.Id))
                .ToListAsync(cancellationToken);

            var subProducts = await _context.Set<Product>()
                .AsTracking()
                .Where(s => request.SubProducts.Contains(s.Id))
                .ToListAsync(cancellationToken);

            var supplier = await _context.Set<Supplier>()
                .AsTracking()
                .Where(s => s.Id == request.SupplierId)
                .SingleOrDefaultAsync(cancellationToken);




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
