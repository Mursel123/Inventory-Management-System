using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Threading;

namespace InventoryManagementSystem.UI.Pages.Ingredients
{
    public partial class IngredientOverview
    {
        [Inject]
        public IClient Client { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private List<IngredientListDto> Ingredients { get; set; } = new();
        private bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            Ingredients = await Client.ReadAllIngredientsAsync();
            IsLoading = false;
        }

        private EventCallback<MudBlazor.DataGridRowClickEventArgs<IngredientListDto>> RowClick()
        {
            return EventCallback.Factory.Create(this, (MudBlazor.DataGridRowClickEventArgs<IngredientListDto> args) =>
            {
                string id = args.Item.Id.ToString();

                NavigationManager.NavigateTo($"/ingredients/{id}");
            });
        }
    }
}
