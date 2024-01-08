using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductUpdateDto : BaseDto
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DocumentDto? Document { get; set; }
        public Guid? SupplierId { get; set; }

        public List<IngredientSelectListDto> Ingredients { get; set; } = new();
        public List<ProductTypeDto> ProductTypes { get; set; } = new();
        public List<ProductSelectListDto> SubProducts { get; set; } = new();
    }
}
