using InventoryManagementSystem.Application.DTOs.Supplier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierSelectList
{
    public class ReadSupplierListQuery : IRequest<List<SupplierDTO>>
    {
    }
}
