using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Settings : BaseEntity
    {
        public int AtLeastProductAmount { get; set; } 
        public decimal AtLeastIngredientMLTotal { get; set; } 
    }
}
