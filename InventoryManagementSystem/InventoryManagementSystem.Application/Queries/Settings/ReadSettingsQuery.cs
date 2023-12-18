using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Settings
{
    public record ReadSettingsQuery : IRequest<Domain.Entities.Settings> { }
}
