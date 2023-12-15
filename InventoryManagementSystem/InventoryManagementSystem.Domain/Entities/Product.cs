using InventoryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Amount { get; set; } 
        public decimal Price { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual Supplier? Supplier { get; set; }
        public virtual Document? Document { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; } = new();
        public virtual List<ProductType> ProductTypes { get; set; } = new();
        public virtual List<OrderLine> OrderLines { get; set; } = new();



    }
}
