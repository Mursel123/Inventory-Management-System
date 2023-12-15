using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
    public class ProductType : BaseEntity
    {
        public string Type { get; set; } = string.Empty;
        public virtual List<Product> Products { get; set; } = new();

    }
}
