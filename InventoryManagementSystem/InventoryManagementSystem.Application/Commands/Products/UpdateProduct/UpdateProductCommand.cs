using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DocumentDto? Document { get; set; }
        public Guid? SupplierId { get; set; }
        public List<Guid> Ingredients { get; set; } = new();
        public List<Guid> SubProducts { get; set; } = new();
        public List<ProductTypeDto> ProductTypes { get; set; } = new();
    }
}
