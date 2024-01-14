using InventoryManagementSystem.Application.DTOs.Price;

namespace InventoryManagementSystem.Application.DTOs.Ingredient
{
    public class IngredientDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal MlUsage { get; set; } //How much ml it is needed for a product.
        public decimal MlTotal { get; set; } //How much ml is in stock of this ingredient.
        public virtual List<PriceListDto> Prices { get; set; } = new();
    }
}
