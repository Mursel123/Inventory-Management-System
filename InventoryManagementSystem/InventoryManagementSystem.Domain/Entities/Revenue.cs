using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
    [NotMapped]
    public class Revenue
    {
        public virtual List<Order> Orders { get; }
        public double PurchasedPercentage { get; private set; } = 0;
        public double SalesPercentage { get; private set; } = 0;
        public double ProfitPercentage { get; private set; } = 0;
        public decimal PurchasedTotal { get; private set; } = 0;
        public decimal SalesTotal { get; private set; } = 0;
        public decimal ProfitTotal { get; private set; } = 0;
        public string MostSoldProduct
        {
            get
            {
                
                return Orders
                    .Where(o => o.Type == OrderType.Sales)
                    .SelectMany(o => o.OrderLines)
                    .GroupBy(ol => ol.Product)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key.Name) 
                    .FirstOrDefault();

            }
        }

        public string LeastSoldProduct
        {
            get
            {

                return Orders
                    .Where(o => o.Type == OrderType.Sales)
                    .SelectMany(o => o.OrderLines)
                    .GroupBy(ol => ol.Product)
                    .OrderBy(g => g.Count())
                    .Select(g => g.Key.Name) 
                    .FirstOrDefault();
            }
        }
        public Revenue(List<Order> orders)
        {
            Orders = orders ?? throw new ArgumentNullException(nameof(orders));
            CalculateOmzet();
        }

        private void CalculateOmzet()
        {
            if (Orders.Count == 0)
            {
                return;
            }

            int totalOrders = Orders.Count;
            int purchasedCount = Orders.Count(o => o.Type == OrderType.Purchased || o.Type == OrderType.Ingredient);
            int salesCount = Orders.Count(o => o.Type == OrderType.Sales);

            PurchasedTotal = Orders
                .Where(o => o.Type == OrderType.Purchased || o.Type == OrderType.Ingredient)
                .Sum(o => o.TotalCost);

            SalesTotal = Orders
                .Where(o => o.Type == OrderType.Sales)
                .Sum(o => o.TotalCost);

            ProfitTotal = SalesTotal - PurchasedTotal;

            PurchasedPercentage = (double)PurchasedTotal / (double)(PurchasedTotal + SalesTotal) * 100;
            SalesPercentage = (double)SalesTotal / (double)(PurchasedTotal + SalesTotal) * 100;
            ProfitPercentage = (double)ProfitTotal / (double)(PurchasedTotal + SalesTotal) * 100;

        }
    }
}
