using FluentValidation;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Domain.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventoryManagementSystem.Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<ProductDto>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Price).GreaterThan(0.01m);
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Description).NotEmpty();

            RuleFor(command => command.Supplier)
                    .NotNull()
                    .When(command => command.ProductTypes.Exists(x => x.Type == ProductTypeData.PurchasedInventory))
                    .WithMessage("Supplier is required when the product type is for purchasing.");
        }
    }
}
