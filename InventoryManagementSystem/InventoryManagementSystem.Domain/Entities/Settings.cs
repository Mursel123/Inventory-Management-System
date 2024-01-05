using InventoryManagementSystem.Domain.Entities.Base;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Settings : BaseEntity
    {
        public int AtLeastProductAmount { get; set; } 
        public decimal AtLeastIngredientMLTotal { get; set; } 
    }
}
