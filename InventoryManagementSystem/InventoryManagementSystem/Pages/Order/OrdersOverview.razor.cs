using InventoryManagementSystem.Application.DTOs.Order;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.Orders.ReadOrderList;
using InventoryManagementSystem.Application.Queries.ReadProductList;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.Pages.Order
{
    public partial class OrdersOverview : IDisposable
    {
        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private ICollection<OrderListDTO>? Orders { get; set; }

        private CancellationTokenSource _cancellationTokenSource = new();

        protected override async Task OnInitializedAsync()
        {
            Orders = await _mediator.Send(new ReadOrderListQuery(), _cancellationTokenSource.Token);

        }
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }


        private EventCallback<MudBlazor.DataGridRowClickEventArgs<OrderListDTO>> RowClick()
        {
            return EventCallback.Factory.Create(this, async (MudBlazor.DataGridRowClickEventArgs<OrderListDTO> args) =>
            {
                NavigationManager.NavigateTo($"/order/{args.Item.Id}");
            });
        }
    }
}
