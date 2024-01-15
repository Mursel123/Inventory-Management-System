using FluentValidation;
using InventoryManagementSystem.UI.Services;

namespace InventoryManagementSystem.UI.Validators.Ingredient
{
    public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} can not be longer then 50 characters.");

            RuleFor(x => x.MlUsage)
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be under 0.");

            RuleFor(x => x.MlTotal)
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be under 0.");
        }
    }
}
