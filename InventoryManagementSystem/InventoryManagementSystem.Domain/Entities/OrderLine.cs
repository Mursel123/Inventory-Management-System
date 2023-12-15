namespace InventoryManagementSystem.Domain.Entities
{
    public class OrderLine : BaseEntity
    {
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Ingredient? Ingredient { get; set; }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                CalculateTotal();
            }
        }

        public decimal Total { get; private set; }



        private void CalculateTotal()
        {
            if (Product != null)
            {
                Total = Quantity * Product.Price;
                Product.Amount -= Quantity;
            }
            else if (Ingredient != null)
            {
                
                Total = Quantity * Ingredient.Prices.FirstOrDefault().IngredientPrice;
                Ingredient.MlTotal -= Quantity * Ingredient.Prices.FirstOrDefault().Ml;
            }
        }
    }
}