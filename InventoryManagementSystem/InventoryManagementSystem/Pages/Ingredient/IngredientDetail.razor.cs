using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientById;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace InventoryManagementSystem.Pages.Ingredient
{
    public partial class IngredientDetail
    {
        [Inject]
        private IMediator _mediator { get; set; }
        [Inject]
        public IJSRuntime JSRuntime  { get; set; }
        [Parameter]
        public string IngredientId { get; set; }
        private IngredientDTO Ingredient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Ingredient = await _mediator.Send(new ReadIngredientByIdQuery() { Id = Guid.Parse(IngredientId) });

        }

    }
}
