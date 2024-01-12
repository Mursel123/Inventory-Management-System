using FluentValidation;
using InventoryManagementSystem.UI.Services;
using InventoryManagementSystem.UI.StaticData;

namespace InventoryManagementSystem.UI.Validators.Product
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} can not be longer then 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("{PropertyName} can not be longer then 50 characters.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be under 0.");

            RuleFor(x => x.Amount)
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be under 0.");

            When(x => x.ProductTypes.Exists(x => x.Type == ProductTypeData.PurchasedInventory), () =>
            {
                RuleFor(x => x.SupplierId)
                .NotNull().WithMessage("When product type purchased is selected, supplier is required.");
            });
        }
    }
}
