using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.ReadProductList
{
    public class ReadProductListQuery : IRequest<List<ProductListDTO>>
    {
        public string ProductType { get; set; } = string.Empty;
    }
}
