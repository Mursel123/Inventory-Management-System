using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Prices
{
    public class DeletePriceCommandHandler : IRequestHandler<DeletePriceCommand, Guid>
    {
        private readonly IDbContext _context;

        public DeletePriceCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeletePriceCommand request, CancellationToken cancellationToken)
        {
            var price = await _context.Set<Price>().SingleAsync(x => x.Id == request.Id, cancellationToken);

            price.IsDeleted = true;

            _context.Set<Price>().Update(price);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
