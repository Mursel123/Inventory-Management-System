using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.UI.Pages.Products
{
    public partial class ProductOverview
    {
        [Inject]
        private IClient Client { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private ICollection<ProductListDto> Products { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Products = await Client.ReadAllProductsAsync();
        }

        private EventCallback<MudBlazor.DataGridRowClickEventArgs<ProductListDto>> RowClick()
        {
            return EventCallback.Factory.Create(this, (MudBlazor.DataGridRowClickEventArgs<ProductListDto> args) =>
            {
                string productId = args.Item.Id.ToString();

                NavigationManager.NavigateTo($"/products/{productId}");
            });
        }
    }
}
