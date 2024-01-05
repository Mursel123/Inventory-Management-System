using InventoryManagementSystem.Domain.Entities.Base;
using InventoryManagementSystem.Domain.Enums;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;

        public decimal TotalCost { get; private set; }

        public virtual Document? Document { get; set; }

        private List<OrderLine> orderLines = new();

        public List<OrderLine> OrderLines
        {
            get { return orderLines; }
            set
            {
                orderLines = value;
                foreach (var line in orderLines)
                {
                    line.CalculateAmountAndTotal();
                }
                CalculateTotal();
            }
        }

        public OrderType Type { get; set; }

        private void CalculateTotal()
        {
            
            TotalCost = orderLines.Sum(x => x.TotalCost);
        }
    }
}
