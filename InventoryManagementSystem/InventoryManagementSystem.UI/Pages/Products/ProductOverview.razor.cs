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
        private List<ProductListDto> Products { get; set; } = new();
        private bool IsLoading { get; set; } = true;
        protected async override Task OnInitializedAsync()
        {
            Products = await Client.ReadAllProductsAsync();
            IsLoading = false;
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
