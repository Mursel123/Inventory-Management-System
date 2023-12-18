using AutoMapper;
using InventoryManagementSystem.Application.Commands.Products.UpdateProduct;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Settings
{
    internal class CreateSettingsCommandHandler : IRequestHandler<CreateSettingsCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public CreateSettingsCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSettingsCommand request, CancellationToken cancellationToken)
        {
            var settings = await _context.Set<Domain.Entities.Settings>().SingleOrDefaultAsync();
            
            if (settings is null)
            {
                
                settings = _mapper.Map<Domain.Entities.Settings>(request);

                await _context.Set<Domain.Entities.Settings>().AddAsync(settings, cancellationToken);
            }
            else
            {
                _mapper.Map(request, settings, typeof(CreateSettingsCommand), typeof(Domain.Entities.Settings));

                _context.Set<Domain.Entities.Settings>().Update(settings);

            }

            await _context.SaveChangesAsync(cancellationToken);

            return settings.Id;
        }
    }
}
