﻿using InventoryManagementSystem.Application.Commands.Analytics.ReadOmzetLineChart;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InventoryManagementSystem.Pages.Analytics
{
    public  partial class LineChart
    {
        [Inject]
        private IMediator _mediator { get; set; }

        private ChartOptions options = new();

        private int selectedYear = DateTime.Now.Year;

        public int SelectedYear
        {
            get { return selectedYear; }
            set 
            { 
                selectedYear = value;
                _ = ReadChartSeries();
                
            }
        }

        private List<int> Years { get; set; } = new() { 2023, 2024 };
        private List<ChartSeries> Series;

        private string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep","Oct","Nov","Dec" };

        protected override async Task OnInitializedAsync()
        {
            options.InterpolationOption = InterpolationOption.Straight;
            options.YAxisFormat = "c2";
            options.YAxisTicks = 50;
            options.ChartPalette = new string[2]{ "#ff574d", "#84fa84"};
            await ReadChartSeries();
        }

        private async Task ReadChartSeries()
        {
            var series = await _mediator.Send(new ReadOmzetLineChartQuery() { Year = SelectedYear });

            Series = series
                .Select(chartSeries => new MudBlazor.ChartSeries
                {
                    Name = chartSeries.Name,
                    Data = chartSeries.Data
                })
                .ToList();
            StateHasChanged();

        }

    }
}
