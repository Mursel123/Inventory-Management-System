using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.DTOs.Supplier;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductDto : BaseDto
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual SupplierDto? Supplier { get; set; }
        public virtual DocumentDto? Document { get; set; }
        public virtual List<ProductIngredientListDto> Ingredients { get; set; } = new();
        public virtual List<ProductTypeDto> ProductTypes { get; set; } = new();
        public virtual List<ProductProductListDto> SubProducts { get; set; } = new();
    }
}
