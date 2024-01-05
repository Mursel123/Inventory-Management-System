using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.OrderLine
{
    public class OrderLineDTO
    {
        public Guid Id { get; set; }
        public ProductListDto? Product { get; set; }
        public IngredientListDTO? Ingredient { get; set; }
        public int? Quantity { get; set; }

    }
}
