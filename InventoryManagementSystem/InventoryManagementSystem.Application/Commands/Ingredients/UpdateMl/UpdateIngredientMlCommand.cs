using InventoryManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.UpdateMl
{
    public class UpdateIngredientMlCommand : IRequest<Ingredient>
    {
        public Ingredient Ingredient { get; set; }
        public decimal MlTotal { get; set; }
        public bool IsIncrement { get; set; }

        public UpdateIngredientMlCommand(Ingredient ingredient, decimal mlTotal, bool isIncrement)
        {
            Ingredient = ingredient;
            MlTotal = mlTotal;
            IsIncrement = isIncrement;
        }
    }
}
