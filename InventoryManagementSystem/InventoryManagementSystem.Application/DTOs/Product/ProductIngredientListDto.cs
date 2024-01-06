namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductIngredientListDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal MlUsage { get; set; }
    }
}
