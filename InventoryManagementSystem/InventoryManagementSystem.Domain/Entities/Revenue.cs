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
        private List<Order> Orders { get; }
        private List<OrderLine> OrderLines { get; }
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

                var product = OrderLines
                    .Where(ol => ol.Order.Type == OrderType.Sales)
                    .GroupBy(ol => ol.Product.Id)
                    .OrderByDescending(g => g.Count())
                    .Select(x => x.FirstOrDefault().Product.Name)
                    .FirstOrDefault();
                return product ?? "No sales orders has been placed";

            }
        }

        public string LeastSoldProduct
        {
            get
            {

                var product =  OrderLines
                    .Where(ol => ol.Order.Type == OrderType.Sales)
                    .GroupBy(ol => ol.Product.Id)
                    .OrderBy(g => g.Count())
                    .Select(x => x.FirstOrDefault().Product.Name)
                    .FirstOrDefault();

                return product ?? "No sales orders has been placed";


            }
        }
        public Revenue(List<Order> orders, List<OrderLine> orderlines)
        {
            Orders = orders;
            OrderLines = orderlines;
            CalculateOmzet();
        }

        private void CalculateOmzet()
        {
            if (Orders.Count == 0)
                return;

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
