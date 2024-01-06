using InventoryManagementSystem.Application.DTOs.Supplier;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierSelectList
{
    public class ReadSupplierSelectListQuery : IRequest<List<SupplierSelectListDto>>
    {
    }
}
