using InventoryManagementSystem.Application.DTOs.Product;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductByType
{
    public class ReadProductListByTypeQuery : IRequest<List<ProductSelectListDto>>
    {
        public string ProductType { get; set; }

        public ReadProductListByTypeQuery(string productType)
        {
            ProductType = productType;
        }
    }
}
