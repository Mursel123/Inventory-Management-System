using InventoryManagementSystem.Application.DTOs.Product;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductList
{
    public class ReadProductListQuery : IRequest<List<ProductListDto>>
    {
    }
}
