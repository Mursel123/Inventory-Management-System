using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using InventoryManagementSystem.UI.Services;
using Blazored.FluentValidation;
using Microsoft.JSInterop;

namespace InventoryManagementSystem.UI.Pages.Ingredients
{
    public partial class UpdateIngredient
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IClient Client { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public string IngredientId { get; set; } = string.Empty;
        private UpdateIngredientCommand Ingredient { get; set; } = new();

        private bool ShowPriceForm { get; set; } = false;
        private PriceListDto? Price { get; set; } = new();

        protected async override Task OnInitializedAsync()
        {
            Ingredient.Prices = new();

            var ingredient = await Client.ReadIngredientByIdAsync(Guid.Parse(IngredientId));
            Ingredient = new()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                MlTotal = ingredient.MlTotal,
                MlUsage = ingredient.MlUsage,
                Prices = ingredient.Prices
            };

        }

        private async Task OnSubmitAsync()
        {
            try
            {
                await Client.UpdateIngredientAsync(Ingredient);
                Snackbar.Add("The ingredient has been successfully updated.", Severity.Success);
                await JSRuntime.InvokeVoidAsync("history.back");

            }
            catch (Exception)
            {
                Snackbar.Add("Failed to update the ingredient. Please try again later.", Severity.Error);
            }

        }
        private void ShowLoadingForm()
        {
            if (!ShowPriceForm)
            {
                ShowPriceForm = true;
            }
        }

        private void Remove(PriceListDto? price)
        {
            if (price != null)
            {
                Ingredient.Prices.Remove(price);

            }
        }
        private void AddPriceToList()
        {
            if (Price != null)
            {
                Ingredient.Prices.Add(Price);
                Price = new();
                ShowPriceForm = false;

            }
        }
    }
}
