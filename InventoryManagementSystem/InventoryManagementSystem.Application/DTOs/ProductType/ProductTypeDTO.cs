using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.ProductType
{
    public class ProductTypeDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
