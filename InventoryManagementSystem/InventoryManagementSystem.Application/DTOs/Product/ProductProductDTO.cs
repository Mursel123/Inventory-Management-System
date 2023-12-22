using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductProductDTO
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
