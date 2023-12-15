using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Ingredient
{
    public class IngredientPriceDTO
    {
        public decimal IngredientPrice { get; set; }
        public decimal Ml { get; set; }
        public string WebsiteLink { get; set; } = string.Empty;
    }
}
