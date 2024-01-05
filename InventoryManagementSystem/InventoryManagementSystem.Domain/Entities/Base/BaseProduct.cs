using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities.Base
{
    public abstract class BaseProduct : BaseEntity
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual Supplier? Supplier { get; set; }
        public virtual Document? Document { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; } = new();
    }
}
