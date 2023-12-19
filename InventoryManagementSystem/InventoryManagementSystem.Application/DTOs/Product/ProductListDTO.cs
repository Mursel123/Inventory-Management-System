using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductListDTO
    {
        public Guid Id { get; set; }
        public int Amount { get; set; } 
        public decimal Price { get; set; } 
        public string Name { get; set; } = string.Empty;
        public virtual List<ProductTypeDTO> ProductTypes { get; set; } = new();
        public virtual List<IngredientListDTO> Ingredients { get; set; } = new();
        public string ProductTypesString { get { return ProductTypeToString(ProductTypes); } }
        private string ProductTypeToString(List<ProductTypeDTO> ProductTypes)
        {
            return string.Join(", ", ProductTypes.Select(pt => pt.Type));
        }

    }
}
