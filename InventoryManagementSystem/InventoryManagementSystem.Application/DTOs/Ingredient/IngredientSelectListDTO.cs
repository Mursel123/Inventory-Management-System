namespace InventoryManagementSystem.Application.DTOs.Ingredient
{
    public class IngredientSelectListDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal MlUsage { get; set; } //How much ml it is needed for a product.
    }
}
