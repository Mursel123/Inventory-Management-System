using InventoryManagementSystem.Application.Commands.Analytics.ReadOmzet;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;
using System.Linq;

namespace InventoryManagementSystem.Pages.Analytics
{
    public partial class PieDiagram
    {
        [Inject]
        private IMediator _mediator { get; set; }

        private ChartOptions options = new ChartOptions();
        private int Index = -1; //default value cannot be 0 -> first selectedindex is 0.
        private double[] diagramData;
        private Revenue Revenue { get; set; }

        string[] labels = { "Aankoop", "Verkoop","Winst" };

        protected override async Task OnInitializedAsync()
        {
            string[] colors = { "#ff574d", "#84fa84", "#5353ec" };
            options.ChartPalette = colors;
            Revenue = await _mediator.Send(new ReadOmzetQuery());
            diagramData = new double[3] {Revenue.PurchasedPercentage, Revenue.SalesPercentage, Revenue.ProfitPercentage};

        }
    }
}
