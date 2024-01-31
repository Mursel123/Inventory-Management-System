using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.UI.Pages.Orders
{
    public partial class OrderOverview
    {
        [Inject]
        private IClient Client { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private List<OrderListDto> Orders { get; set; } = new();
        private bool IsLoading { get; set; } = true;
        protected async override Task OnInitializedAsync()
        {
            Orders = await Client.ReadOrdersAsync();
            IsLoading = false;
        }

        private EventCallback<MudBlazor.DataGridRowClickEventArgs<OrderListDto>> RowClick()
        {
            return EventCallback.Factory.Create(this, (MudBlazor.DataGridRowClickEventArgs<OrderListDto> args) =>
            {
                string id = args.Item.Id.ToString();

                NavigationManager.NavigateTo($"/orders/{id}");
            });
        }
    }
}
