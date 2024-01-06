using InventoryManagementSystem.Application.DTOs.Supplier;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierList
{
    public class ReadSupplierListQuery : IRequest<List<SupplierDto>>
    {
    }
}
