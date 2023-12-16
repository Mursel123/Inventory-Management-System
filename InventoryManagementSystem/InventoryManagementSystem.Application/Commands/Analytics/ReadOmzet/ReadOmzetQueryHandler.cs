using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Analytics.ReadOmzet
{
    internal class ReadOmzetQueryHandler : IRequestHandler<ReadOmzetQuery, Revenue>
    {
        private readonly IDbContext _context;

        public ReadOmzetQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Revenue> Handle(ReadOmzetQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Set<Order>().Include(x =>  x.OrderLines).ThenInclude(x => x.Product).ToListAsync(cancellationToken);
            return new Revenue(orders);
        }
    }
}
