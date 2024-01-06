using InventoryManagementSystem.Application.DTOs.Ingredient;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList
{
    public class ReadIngredientListQuery : IRequest<List<IngredientListDto>>
    {
    }
}
