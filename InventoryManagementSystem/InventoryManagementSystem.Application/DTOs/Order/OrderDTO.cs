using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.OrderLine;
using InventoryManagementSystem.Domain.Enums;

namespace InventoryManagementSystem.Application.DTOs.Order
{
    public class OrderDTO
    {
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public DocumentDto? Document { get; set; }
        public List<OrderLineDTO> OrderLines { get; set; } = new();
        public OrderType? Type { get; set; }
    }
}
