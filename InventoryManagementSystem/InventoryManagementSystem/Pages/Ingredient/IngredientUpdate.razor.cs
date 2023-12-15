using InventoryManagementSystem.Application.Commands.Ingredients;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Price;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientById;
using InventoryManagementSystem.Application.Commands.Ingredients.Create;
using InventoryManagementSystem.Application.Commands.Ingredients.Update;
using InventoryManagementSystem.Application.Commands.Prices;

namespace InventoryManagementSystem.Pages.Ingredient
{
    public partial class IngredientUpdate
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }

        [Parameter]
        public string IngredientId { get; set; }
        private IngredientDTO Ingredient { get; set; }

        private bool ShowLoading { get; set; } = false;
        private PriceListDTO? Price { get; set; } = new();

        protected async override Task OnInitializedAsync()
        {
            Ingredient = await _mediator.Send(new ReadIngredientByIdQuery() { Id = Guid.Parse(IngredientId) });
        }

        private async Task OnValidSubmit(EditContext context)
        {

            try
            {
                var command = new UpdateIngredientCommand(
                        Ingredient.Id,
                        Ingredient.Name,
                        Ingredient.MlUsage,
                        Ingredient.MlTotal,
                        Ingredient.Prices
                    );

                await _mediator.Send(command);

                Snackbar.Add("The ingredient is updated succesfully", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar.Add("The ingredient could not be updated", Severity.Error);
            }

        }
        private void ShowLoadingForm()
        {
            if (!ShowLoading)
            {
                ShowLoading = true;
            }
        }

        private async Task Remove(PriceListDTO? price)
        {
            if (price != null)
            {
                Ingredient.Prices.Remove(price);
                await _mediator.Send(new DeletePriceCommand(price.Id));

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
