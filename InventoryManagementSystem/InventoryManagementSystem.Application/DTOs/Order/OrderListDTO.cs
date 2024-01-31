using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;

namespace InventoryManagementSystem.Application.DTOs.Order
{
    public class OrderListDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal TotalCost { get; set; }
        public OrderType Type { get; set; }

    }
}
