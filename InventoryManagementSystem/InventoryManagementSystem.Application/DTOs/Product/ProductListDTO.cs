using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.ProductType;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductListDto : BaseDto
    {
        public int Amount { get; set; }
        public decimal Price { get; set; } 
        public string Name { get; set; } = string.Empty;
        public virtual List<ProductTypeDTO> ProductTypes { get; set; } = new();
    }
}
