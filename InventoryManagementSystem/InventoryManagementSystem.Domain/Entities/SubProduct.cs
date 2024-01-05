namespace InventoryManagementSystem.Domain.Entities
{
    public class SubProduct : BaseEntity
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual Supplier? Supplier { get; set; }
        public virtual Document? Document { get; set; }
        public virtual List<Product> Products { get; set; } = new ();
        public virtual List<OrderLine> OrderLines { get; set; } = new();
    }
}
