using InventoryManagementSystem.Application.DTOs.Price;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Ingredient
{
    public class IngredientDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MlUsage { get; set; } //How much ml it is needed for a product.
        public decimal MlTotal { get; set; } //How much ml is in stock of this ingredient.
        public virtual List<PriceListDTO> Prices { get; set; } = new();
    }
}
