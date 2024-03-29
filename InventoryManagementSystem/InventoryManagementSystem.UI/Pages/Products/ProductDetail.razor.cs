﻿using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.UI.Pages.Products
{
    public partial class ProductDetail
    {
        [Inject]
        private IClient Client { get; set; }
        [Parameter]
        public string ProductId { get; set; } = string.Empty;
        private ProductDto Product { get; set; } = new();
        private bool IsLoading { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            Product = await Client.ReadProductByIdAsync(Guid.Parse(ProductId));
            IsLoading = false;
        }
    }
}
