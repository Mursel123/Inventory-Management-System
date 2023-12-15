using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Delete
{
    public class DeleteIngredientCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
