﻿using InventoryManagementSystem.Application.DTOs.Price;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Ingredients.Update
{
    public class UpdateIngredientCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal MlUsage { get; set; } //How much ml it is needed for a product.
        public decimal MlTotal { get; set; } //How much ml is in stock of this ingredient.
        public virtual List<PriceListDto> Prices { get; set; }

        public UpdateIngredientCommand(Guid id, string name, decimal mlUsage, decimal mlTotal, List<PriceListDto> prices)
        {
            Id = id;
            Name = name;
            MlUsage = mlUsage;
            MlTotal = mlTotal;
            Prices = prices;
        }
    }
}
