using InventoryManagementSystem.Domain.Entities.Base;

namespace InventoryManagementSystem.Domain.Entities
{
    public class SubProduct : BaseProduct
    {
        public virtual List<Product> Products { get; set; } = new ();

    }
}
