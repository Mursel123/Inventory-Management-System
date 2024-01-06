using InventoryManagementSystem.Application.DTOs.Ingredient;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientSelectList
{
    public class ReadIngredientSelectListQuery : IRequest<List<IngredientSelectListDto>>
    {
    }
}
