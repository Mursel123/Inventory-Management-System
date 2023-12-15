using InventoryManagementSystem.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Orders.ReadOrderList
{
    public class ReadOrderListQuery : IRequest<List<OrderListDTO>>
    {
    }
}
