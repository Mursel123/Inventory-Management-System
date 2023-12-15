using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.ReadProductById;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.Pages.Product
{
    public partial class ProductDetail
    {
        [Inject]
        private IMediator _mediator { get; set; }
        [Parameter] 
        public string ProductId { get; set; }
        private ProductDTO Product { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Product = await _mediator.Send(new ReadProductByIdQuery() { Id = Guid.Parse(ProductId)});

        }
    }
}
