using InventoryManagementSystem.Domain.Entities.Base;

namespace InventoryManagementSystem.Domain.Entities
{
    public class ProductType : BaseEntity
    {
        public string Type { get; set; } = string.Empty;
        public virtual List<Product> Products { get; set; } = new();

    }
}
