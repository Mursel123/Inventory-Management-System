using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.OrderLine;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using MediatR;

namespace InventoryManagementSystem.Application.Commands.Orders.Create
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DocumentDTO? Document { get; set; }
        public List<OrderLineDTO> OrderLines { get; set; }
        public OrderType? Type { get; set; }

        public CreateOrderCommand(string orderNumber,  DocumentDTO? document, List<OrderLineDTO> orderLines, OrderType? type)
        {
            OrderNumber = orderNumber;
            Document = document;
            OrderLines = orderLines;
            Type = type;
        }
    }
}
