namespace InventoryManagementSystem.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public virtual List<Product> Products { get; set; } = new();
        public virtual List<SubProduct> SubProducts { get; set; } = new();
    }
}
