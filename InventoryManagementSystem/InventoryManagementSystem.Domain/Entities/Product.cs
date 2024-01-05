using InventoryManagementSystem.Domain.Entities.Base;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Product : BaseProduct
    {
        public virtual List<Ingredient> Ingredients { get; set; } = new();
        public virtual List<ProductType> ProductTypes { get; set; } = new();
        public virtual List<SubProduct> SubProducts { get; set; } = new();

    }
}
