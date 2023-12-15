using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Prices
{
    public class DeletePriceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public DeletePriceCommand(Guid id)
        {
            Id = id;
        }
    }
}
