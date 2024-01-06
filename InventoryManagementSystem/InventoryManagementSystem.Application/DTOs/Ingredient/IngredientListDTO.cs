using InventoryManagementSystem.Application.DTOs.Price;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Ingredient
{
    public class IngredientListDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal MlUsage { get; set; } 
        public decimal MlTotal { get; set; }
        public List<PriceListDTO> Prices { get; set; } = new();

    }
}
