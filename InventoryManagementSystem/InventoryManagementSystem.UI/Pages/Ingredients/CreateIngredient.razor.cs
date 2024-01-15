using Blazored.FluentValidation;
using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace InventoryManagementSystem.UI.Pages.Ingredients
{
    public partial class CreateIngredient
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IClient Client { get; set; }

        private CreateIngredientCommand Ingredient { get; set; } = new();
        private bool ShowPriceForm { get; set; } = false;
        private PriceListDto? Price { get; set; } = new();

        protected override void OnInitialized()
        {
            Ingredient.Prices = new();
        }

        private async Task OnSubmitAsync(EditContext context)
        {
            try
            {
                await Client.CreateIngredientAsync(Ingredient);
                Snackbar.Add("The ingredient has been successfully added.", Severity.Success);

            }
            catch (Exception)
            {
                Snackbar.Add("Failed to add the ingredient. Please try again later.", Severity.Error);
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
