﻿using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.OrderLine;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using MediatR;

namespace InventoryManagementSystem.Application.Commands.Orders.Create
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public DocumentDTO? Document { get; set; }
        public List<OrderLineDTO> OrderLines { get; set; }
        public OrderType? Type { get; set; }

        public CreateOrderCommand(DocumentDTO? document, List<OrderLineDTO> orderLines, OrderType? type)
        {
            Document = document;
            OrderLines = orderLines;
            Type = type;
        }
    }
}
