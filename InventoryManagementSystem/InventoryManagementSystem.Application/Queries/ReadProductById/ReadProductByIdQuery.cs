using InventoryManagementSystem.Application.DTOs.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.ReadProductById
{
    public class ReadProductByIdQuery : IRequest<ProductDTO>
    {
        public Guid Id { get; set; }
    }
}
