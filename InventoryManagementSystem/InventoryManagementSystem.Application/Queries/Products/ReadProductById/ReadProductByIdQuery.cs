using InventoryManagementSystem.Application.DTOs.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Products.ReadProductById
{
    public class ReadProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        public ReadProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
