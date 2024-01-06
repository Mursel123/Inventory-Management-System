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
        public string Name { get; set; }
        public string Description { get; set; }

        public DocumentDto? Document { get; set; }
        public List<IngredientListDto> Ingredients { get; set; }

        public SupplierDto? Supplier { get; set; }

        public List<ProductTypeDto> ProductTypes { get; set; }

        public UpdateProductCommand(Guid id, int amount, decimal price, string name, string description, DocumentDto? document, List<IngredientListDto> ingredients, SupplierDto? supplier, List<ProductTypeDto> productTypes)
        {
            Id = id;
            Amount = amount;
            Price = price;
            Name = name;
            Description = description;
            Document = document;
            Ingredients = ingredients;
            Supplier = supplier;
            ProductTypes = productTypes;
        }
    }
}
