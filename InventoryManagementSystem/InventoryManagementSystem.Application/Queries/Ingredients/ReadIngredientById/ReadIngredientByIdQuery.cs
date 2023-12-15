using InventoryManagementSystem.Application.DTOs.Ingredient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientById
{
    public class ReadIngredientByIdQuery : IRequest<IngredientDTO>
    {
        public Guid Id { get; set; }
    }
}
