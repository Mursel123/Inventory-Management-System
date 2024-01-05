namespace InventoryManagementSystem.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal MlUsage { get; set; } //How much ml it is needed for a product.
        public decimal MlTotal { get; set; } //How much total ml is in stock.
        public virtual List<Product> Products { get; set; } = new();
        public virtual List<Price> Prices { get; set; } = new();
        public virtual List<OrderLine> OrderLines { get; set; } = new();

    }
}
