using InventoryManagementSystem.Application.DTOs.Order;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Orders.ReadOrderList
{
    public class ReadOrderListQuery : IRequest<IReadOnlyList<OrderListDto>> 
    { 

    };
}
