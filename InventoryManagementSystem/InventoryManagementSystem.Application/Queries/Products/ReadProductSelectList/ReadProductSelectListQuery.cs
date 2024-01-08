using InventoryManagementSystem.Application.DTOs.Product;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductSelectList
{
    public class ReadProductSelectListQuery : IRequest<List<ProductSelectListDto>>
    {
        public string ProductType { get; set; } = string.Empty;

        public ReadProductSelectListQuery(string productType)
        {
            ProductType = productType;
        }
    }
}
