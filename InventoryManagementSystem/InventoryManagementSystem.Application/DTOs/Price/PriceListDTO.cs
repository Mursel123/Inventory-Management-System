namespace InventoryManagementSystem.Application.DTOs.Price
{
    public class PriceListDto : BaseDto
    {
        public decimal IngredientPrice { get; set; }
        public decimal Ml { get; set; } //Ml of the ingredient bought.
        public string WebsiteLink { get; set; } = string.Empty;
    }
}
