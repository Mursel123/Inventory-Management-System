using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.UI.Pages.Ingredients
{
    public partial class IngredientDetail
    {
        [Inject]
        private IClient Client { get; set; }
        [Parameter]
        public string IngredientId { get; set; } = string.Empty;
        private IngredientDto Ingredient { get; set; } = new();
        private bool IsLoading { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            Ingredient = await Client.ReadIngredientByIdAsync(Guid.Parse(IngredientId));
            IsLoading = false;
        }
    }
}
