using AutoMapper;
using FluentValidation;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.StaticData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateProductCommand> _validator;
        public UpdateProductCommandHandler(IDbContext context, IMapper mapper, IValidator<UpdateProductCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (result.Errors.Any())
                throw new Exceptions.ValidationException(result);

            CheckForProductType(request);

            var product = await _context.Set<Product>()
                .AsTracking()
                .Include(x => x.Supplier)
                .Include(x => x.Ingredients)
                .Include(x => x.ProductTypes)
                .Include(x => x.SubProducts)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            _mapper.Map(request, product, typeof(UpdateProductCommand), typeof(Product));

            var ingredients = await _context.Set<Ingredient>()
                .AsTracking()
                .Where(s => request.Ingredients.Contains(s.Id))
                .ToListAsync(cancellationToken);

            var types = await _context.Set<ProductType>()
                .AsTracking()
                .Where(s => request.ProductTypes.Select(rs => rs.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            var supplier = await _context.Set<Supplier>()
                .AsTracking()
                .Where(s => s.Id == request.SupplierId)
                .SingleOrDefaultAsync(cancellationToken);

            var subProducts = await _context.Set<Product>()
                .AsTracking()
                .Where(s => request.SubProducts.Contains(s.Id))
                .ToListAsync(cancellationToken);

            product.Ingredients = ingredients;
            product.Supplier = supplier;
            product.ProductTypes = types;
            product.SubProducts = subProducts;

            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            
            return product.Id;
        }

        private void CheckForProductType(UpdateProductCommand request)
        {
            if (!request.ProductTypes.Exists(x => x.Type == ProductTypeData.SalesInventory))
            {
                request.Ingredients = new();
                request.SubProducts = new();
                request.Document = null;

            }
            if (!request.ProductTypes.Exists(x => x.Type == ProductTypeData.PurchasedInventory))
            {
                request.SupplierId = null;
            }
        }

       
    }
}
