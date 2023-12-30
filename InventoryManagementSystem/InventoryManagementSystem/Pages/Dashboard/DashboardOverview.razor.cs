using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.Dashboard.ReadIngredientProductAlertList;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.Pages.Dashboard
{
    public partial class DashboardOverview
    {
        [Inject]
        private IMediator Mediator { get; set; }
        public List<ProductListDTO> Products { get; set; } = new();
        public List<IngredientListDTO> Ingredients { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(new ReadIngredientProductAlertListQuery());
            Products = result.Item1;
            Ingredients = result.Item2;
        }
    }
}
