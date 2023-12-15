using InventoryManagementSystem.Application.DTOs.Price;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Create
{
    public class CreateIngredientCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public decimal MlUsage { get; set; } //How much ml it is needed for a product.
        public decimal MlTotal { get; set; } //How much ml is in stock of this ingredient.
        public virtual List<PriceListDTO> Prices { get; set; }

        public CreateIngredientCommand(string name, decimal mlUsage, decimal mlTotal, List<PriceListDTO> prices)
        {
            Name = name;
            MlUsage = mlUsage;
            MlTotal = mlTotal;
            Prices = prices;
        }
    }
}
