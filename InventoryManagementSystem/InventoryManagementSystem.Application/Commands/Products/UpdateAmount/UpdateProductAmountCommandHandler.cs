using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Products.UpdateAmount
{
    internal class UpdateProductAmountCommandHandler : IRequestHandler<UpdateProductAmountCommand, Product>
    {
        private readonly IDbContext _context;

        public UpdateProductAmountCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(UpdateProductAmountCommand request, CancellationToken cancellationToken)
        {
            //var product = await _context.Set<Product>().AsTracking().SingleAsync(x => x.Id == request.Product.Id);
            request.Product.Amount = request.IsIncrement ?
                    request.Product.Amount + request.Amount :
                    request.Product.Amount - request.Amount;

            //_context.Set<Product>().Update(request.Product);
            //await _context.SaveChangesAsync(cancellationToken);
            /*await _context.Set<Product>()
                .Where(x => x.Id == request.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.Amount,
                    x => request.IsIncrement ?
                    x.Amount + request.Amount :
                    x.Amount - request.Amount), cancellationToken);*/

            return request.Product;
        }
    }
}
