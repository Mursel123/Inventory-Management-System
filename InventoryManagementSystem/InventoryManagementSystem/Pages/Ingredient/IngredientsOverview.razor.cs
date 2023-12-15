using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Threading;

namespace InventoryManagementSystem.Pages.Ingredient
{
    public partial class IngredientsOverview : IDisposable
    {
        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private ICollection<IngredientListDTO> Ingredients { get; set; }
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected override async Task OnInitializedAsync()
        {

            Ingredients = await _mediator.Send(new ReadIngredientListQuery(), _cancellationTokenSource.Token);

        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
        private EventCallback<MudBlazor.DataGridRowClickEventArgs<IngredientListDTO>> RowClick()
        {
            return EventCallback.Factory.Create(this, async (MudBlazor.DataGridRowClickEventArgs<IngredientListDTO> args) =>
            {
                string id = args.Item.Id.ToString();

                NavigationManager.NavigateTo($"/ingredient/{id}");
            });
        }
    }
}
