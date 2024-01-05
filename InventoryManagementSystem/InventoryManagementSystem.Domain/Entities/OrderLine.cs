namespace InventoryManagementSystem.Domain.Entities
{
    public class OrderLine : BaseEntity
    {
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public virtual SubProduct? SubProduct { get; set; }
        public virtual Ingredient? Ingredient { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; private set; }

        internal void CalculateAmountAndTotal()
        {
            if (Product != null)
            {
                TotalCost = Quantity * Product.Price;
                    
            }
            else if (Ingredient != null)
            {
                TotalCost = Quantity * Ingredient.Prices[0].IngredientPrice;

            }
            else if (SubProduct != null)
            {
                TotalCost = Quantity * SubProduct.Price;
            }
        }
    }
}