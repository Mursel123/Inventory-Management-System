using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Entities.NotMapped;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Analytics.ReadOmzetLineChart
{
    internal class ReadOmzetLineQueryHandler : IRequestHandler<ReadOmzetLineChartQuery, List<ChartSeries>>
    {
        private readonly IDbContext _context;

        public ReadOmzetLineQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChartSeries>> Handle(ReadOmzetLineChartQuery request, CancellationToken cancellationToken)
        {
            List<ChartSeries> result = new()
            {
                await ReadTotalCostPerYear(request.Year, "Aankoop", new(){OrderType.Purchased,OrderType.Ingredient }, cancellationToken),
                await ReadTotalCostPerYear(request.Year, "Verkoop", new(){OrderType.Sales }, cancellationToken)
            };
            return result;
        }

        private async Task<ChartSeries> ReadTotalCostPerYear(int year, string name, List<OrderType> types, CancellationToken cancellationToken)
        {
            var monthlyTotalCosts = await _context.Set<Order>()
                .Where(x => x.Date.Year == year && types.Contains(x.Type))
                .GroupBy(x => x.Date.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    TotalCost = group.Sum(x => x.TotalCost)
                })
                .ToListAsync(cancellationToken);

            var chartSeries = new ChartSeries
            {
                Name = name,
                Data = new double[12]
            };

            foreach (var monthlyTotal in monthlyTotalCosts)
            {
                // Assuming that month indices start from 1
                chartSeries.Data[monthlyTotal.Month - 1] = (double)monthlyTotal.TotalCost;
            }

            return chartSeries;
        }


    }
}
