﻿using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.ReadProductList;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading;

namespace InventoryManagementSystem.Pages.Product
{
    public partial class ProductsOverview : IDisposable
    {
        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private ICollection<ProductListDto> Products { get; set; }

        private ProductListDto? selectedProduct = null;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected override async Task OnInitializedAsync()
        {
            Products = await _mediator.Send(new ReadProductListQuery(), _cancellationTokenSource.Token);

        }
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }


        private EventCallback<MudBlazor.DataGridRowClickEventArgs<ProductListDto>> RowClick()
        {
            return EventCallback.Factory.Create(this, async (MudBlazor.DataGridRowClickEventArgs<ProductListDto> args) =>
            {
                string productId = args.Item.Id.ToString();

                NavigationManager.NavigateTo($"/product/{productId}");
            });
        }
    }
}
