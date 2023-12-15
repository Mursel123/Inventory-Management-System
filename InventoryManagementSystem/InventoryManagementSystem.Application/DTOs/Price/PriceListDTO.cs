using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Price
{
    public class PriceListDTO
    {
        public Guid Id { get; set; }
        public decimal IngredientPrice { get; set; }
        public decimal Ml { get; set; } //Ml of the ingredient bought.
        public string WebsiteLink { get; set; } = string.Empty;
    }
}
