using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Settings
{
    public class CreateSettingsCommand : IRequest<Guid>
    {
        public int AtLeastProductAmount { get; set; }
        public decimal AtLeastIngredientMLTotal { get; set; }

        public CreateSettingsCommand(int atLeastProductAmount, decimal atLeastIngredientMLTotal)
        {

            AtLeastProductAmount = atLeastProductAmount;
            AtLeastIngredientMLTotal = atLeastIngredientMLTotal;
        }
    }
}
