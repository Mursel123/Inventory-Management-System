using InventoryManagementSystem.Domain.Enums;

namespace InventoryManagementSystem.Domain.Entities
{
    public class OrderLine : BaseEntity
    {
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Ingredient? Ingredient { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; private set; }

        internal void CalculateAmountAndTotal()
        {
            if (Product != null)
            {
                Total = Quantity * Product.Price;
                    
            }
            else if (Ingredient != null)
            {
                Total = Quantity * Ingredient.Prices.FirstOrDefault().IngredientPrice;

            }
        }
    }
}