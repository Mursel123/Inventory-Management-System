using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Settings
{
    internal class ReadSettingsQuerHandler : IRequestHandler<ReadSettingsQuery, Domain.Entities.Settings>
    {
        private readonly IDbContext _context;

        public ReadSettingsQuerHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Settings> Handle(ReadSettingsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Domain.Entities.Settings>()
                    .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
