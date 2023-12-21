using InventoryManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Products.UpdateAmount
{
    public class UpdateProductAmountCommand : IRequest<Product>
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public bool IsIncrement { get; set; }

        public UpdateProductAmountCommand(Product product, int amount, bool isIncrement)
        {
            Product = product;
            Amount = amount;
            IsIncrement = isIncrement;
        }
    }
}
