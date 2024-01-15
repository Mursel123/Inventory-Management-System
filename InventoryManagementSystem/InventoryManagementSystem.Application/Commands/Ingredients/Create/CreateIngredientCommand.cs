using InventoryManagementSystem.Application.DTOs.Price;
using MediatR;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Create
{
    public class CreateIngredientCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public decimal MlUsage { get; set; } //How much ml it is needed for a product.
        public decimal MlTotal { get; set; } //How much ml is in stock of this ingredient.
        public List<PriceListDto> Prices { get; set; } = new();

    }
}
