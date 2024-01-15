using FluentValidation;
using InventoryManagementSystem.UI.Services;

namespace InventoryManagementSystem.UI.Validators.Ingredient
{
    public class PriceDtoValidator : AbstractValidator<PriceListDto>
    {
        public PriceDtoValidator()
        {
            RuleFor(x => x.IngredientPrice)
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be under 0.");

            RuleFor(x => x.Ml)
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be under 0.");
        }
    }
}
