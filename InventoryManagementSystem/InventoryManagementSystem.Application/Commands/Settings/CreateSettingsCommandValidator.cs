using FluentValidation;

namespace InventoryManagementSystem.Application.Commands.Settings
{
    public class CreateSettingsCommandValidator : AbstractValidator<Domain.Entities.Settings>
    {
        public CreateSettingsCommandValidator()
        {
            RuleFor(x => x.AtLeastProductAmount).NotNull().WithMessage("Product amount can not be empty");
            RuleFor(x => x.AtLeastIngredientMLTotal).NotNull().WithMessage("Ingredient ml total can not be empty");
        }
    }
}
