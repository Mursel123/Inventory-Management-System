using InventoryManagementSystem.Application.DTOs.Ingredient;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductSelectListDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool OutOfStock { get; set; }
        public virtual List<IngredientListDto> Ingredients { get; set; } = new();
    }
}
