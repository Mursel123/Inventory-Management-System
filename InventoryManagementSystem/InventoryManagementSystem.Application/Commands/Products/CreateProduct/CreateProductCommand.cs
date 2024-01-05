using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.DTOs.Supplier;
using MediatR;

namespace InventoryManagementSystem.Application.Commands.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DocumentDTO? Document { get; set; }
        public List<IngredientListDTO> Ingredients { get; set; }

        public SupplierDTO? Supplier { get; set; }

        public List<ProductTypeDTO> ProductTypes { get; set; }
        public List<ProductListDto> Products { get; set; }
        public CreateProductCommand(int amount, decimal price, string name, string description, DocumentDTO? document, List<IngredientListDTO> ingredients, SupplierDTO? supplier, List<ProductTypeDTO> productTypes, List<ProductListDto> productList)
        {
            Amount = amount;
            Price = price;
            Name = name;
            Description = description;
            Document = document;
            Ingredients = ingredients;
            Supplier = supplier;
            ProductTypes = productTypes;
            Products = productList;
        }
    }
}
