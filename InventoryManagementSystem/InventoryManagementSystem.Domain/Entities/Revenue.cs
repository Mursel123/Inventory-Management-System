using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
    [NotMapped]
    public class Revenue
    {
        public virtual List<Order> Orders { get; } = new();
        public double PurchasedPercentage { get; private set; }
        public double SalesPercentage { get; private set; }
        public double PurchasedTotal { get; private set; }
        public double SalesTotal { get; private set; }
        public double RevenueTotal { get; private set; }

        private void CalculateOmzet()
        {
            int totalOrders = Orders.Count;
            int purchasedCount = Orders.Count(o => o.Type == OrderType.Purchased || o.Type == OrderType.Ingredient);
            int salesCount = Orders.Count(o => o.Type == OrderType.Sales);

            PurchasedPercentage = (double)purchasedCount / totalOrders * 100;
            SalesPercentage = (double)salesCount / totalOrders * 100;


            PurchasedTotal = Orders
                .Where(o => o.Type == OrderType.Purchased || o.Type == OrderType.Ingredient)
                .Sum(o => Convert.ToDouble(o.TotalCost));

            SalesTotal = Orders
                .Where(o => o.Type == OrderType.Sales)
                .Sum(o => Convert.ToDouble(o.TotalCost));

            RevenueTotal = SalesTotal - PurchasedTotal;
        }

        public Revenue(List<Order> orders)
        {
            Orders = orders;
            CalculateOmzet();
        }
    }
}
