using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.UI.Pages.Products
{
    public partial class ProductDetail
    {
        [Inject]
        private IClient Client { get; set; }
        [Parameter]
        public string ProductId { get; set; }
        private ProductDto Product { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Product = await Client.ReadProductByIdAsync(Guid.Parse(ProductId));

        }
    }
}
