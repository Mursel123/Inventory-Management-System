using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.DTOs.Supplier;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public virtual SupplierDTO? Supplier { get; set; }
        public virtual DocumentDTO? Document { get; set; }
        public virtual List<IngredientListDTO> Ingredients { get; set; } = new();
        public virtual List<ProductTypeDTO> ProductTypes { get; set; } = new();
        public virtual List<ProductOrderLineDTO> OrderLines { get; set; } = new();
        public virtual List<ProductListDTO> Products { get; set; } = new();
    }
}
