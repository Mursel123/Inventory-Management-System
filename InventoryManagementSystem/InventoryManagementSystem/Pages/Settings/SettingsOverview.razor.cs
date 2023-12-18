using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Application.Commands.Settings;
using InventoryManagementSystem.Application.Queries.Settings;

using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace InventoryManagementSystem.Pages.Settings
{
    public partial class SettingsOverview
    {
        [Inject]
        private IMediator Mediator { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }


        public Domain.Entities.Settings Settings { get; set; }


        protected async override Task OnInitializedAsync()
        {
            Settings = await Mediator.Send(new ReadSettingsQuery());
            Settings ??= new();
        }

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {

                var command = new CreateSettingsCommand(
                    Settings.AtLeastProductAmount,
                    Settings.AtLeastIngredientMLTotal);

               await Mediator.Send(command);

                Snackbar.Add("The settings has been updated succesfully", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar.Add("The settings could not been updated", Severity.Error);
            }

        }
    }
}
