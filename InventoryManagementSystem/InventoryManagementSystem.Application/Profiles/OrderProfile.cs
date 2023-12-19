using AutoMapper;
using InventoryManagementSystem.Application.Commands.Orders.Create;
using InventoryManagementSystem.Application.DTOs.Order;
using InventoryManagementSystem.Application.DTOs.OrderLine;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Profiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderListDTO>();

            CreateMap<CreateOrderCommand, Order>()
                .BeforeMap((src, dest) => dest.Type = src.Type ?? throw new ArgumentNullException(nameof(src.Type)));
                //.ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

            CreateMap<OrderLineDTO, OrderLine>();
        }
    }
}
