using InventoryManagementSystem.Application.Commands.Ingredients;
using InventoryManagementSystem.Application.Commands.Ingredients.Create;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Price;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace InventoryManagementSystem.Pages.Ingredient
{
    public partial class CreateIngredient
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }
        private IngredientDTO Ingredient { get; set; } = new();

        private bool ShowLoading { get; set; } = false;
        private PriceListDTO? Price { get; set; } = new();


        private async Task OnValidSubmit(EditContext context)
        {

            try
            {
                var command = new CreateIngredientCommand(
                        Ingredient.Name,
                        Ingredient.MlUsage,
                        Ingredient.MlTotal,
                        Ingredient.Prices
                    );

                await _mediator.Send(command);

                Snackbar.Add("The ingredient is added succesfully", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar.Add("The ingredient could not be added", Severity.Error);
            }

        }
        private void ShowLoadingForm()
        {
            if (!ShowLoading)
            {
                ShowLoading = true;
            }
        }

        private void Remove(PriceListDTO? price)
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
                ShowLoading = false;

            }
        }
    }
}
