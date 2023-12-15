using InventoryManagementSystem.Application.DTOs.Price;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Prices.ReadPriceList
{
    public class ReadPriceListQuery : IRequest<List<PriceListDTO>>
    {
        public Guid Id { get; set; }
    }
}
