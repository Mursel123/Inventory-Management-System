using InventoryManagementSystem.Domain.Entities.NotMapped;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Analytics.ReadOmzetLineChart
{
    public class ReadOmzetLineChartQuery : IRequest<List<ChartSeries>>
    {
        public int Year { get; set; }
    }
}
