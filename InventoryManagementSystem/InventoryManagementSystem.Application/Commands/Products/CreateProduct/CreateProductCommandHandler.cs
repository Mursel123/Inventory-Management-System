using AutoMapper;
using FluentValidation;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.StaticData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductCommand> _validator;
        public CreateProductCommandHandler(IDbContext context, IMapper mapper, IValidator<CreateProductCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request ,cancellationToken);

            if (result.Errors.Any())
                throw new Exceptions.ValidationException(result);

            CheckForProductType(request);

            var product = _mapper.Map<Product>(request);

            var ingredients = await _context.Set<Ingredient>()
                .AsTracking()
                .Where(s => request.Ingredients.Select(dto => dto.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            var types = await _context.Set<ProductType>()
                .AsTracking()
                .Where(s => request.ProductTypes.Select(dto => dto.Id).Contains(s.Id))
                .ToListAsync(cancellationToken);

            var subProducts = await _context.Set<Product>()
                .AsTracking()
                .Where(s => request.SubProducts.Select(dto => dto.Id).Contains(s.Id))
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

        private void CheckForProductType(CreateProductCommand request)
        {
            if (!request.ProductTypes.Exists(x => x.Type == ProductTypeData.SalesInventory))
            {
                request.Ingredients = new();
                request.Document = null;

            }
            if (!request.ProductTypes.Exists(x => x.Type == ProductTypeData.PurchasedInventory))
            {
                request.SupplierId = null;
            }
        }
    }
}
