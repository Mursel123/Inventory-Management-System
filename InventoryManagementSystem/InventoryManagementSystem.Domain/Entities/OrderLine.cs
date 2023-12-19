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



        internal void CalculateAmountAndTotal(OrderType type)
        {
            if (Product != null)
            {
                Total = Quantity * Product.Price;

                if (type == OrderType.Purchased)
                    Product.Amount += Quantity;

                if (type == OrderType.Sales)
                {
                    Product.Amount -= Quantity;

                    if (Product.Ingredients.Any())
                    {
                        foreach (var ingredient in Product.Ingredients)
                        {
                            ingredient.MlTotal -= Quantity * ingredient.MlUsage;
                        }
                        
                    }
                }
                    
            }
            else if (Ingredient != null)
            {
                
                Total = Quantity * Ingredient.Prices.FirstOrDefault().IngredientPrice;
                
                    Ingredient.MlTotal -= Quantity * Ingredient.Prices.FirstOrDefault().Ml;
            }
        }
    }
}