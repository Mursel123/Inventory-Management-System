using MudBlazor;

namespace InventoryManagementSystem.Pages.Analytics
{
    public  partial class LineChart
    {
        private int SelectedYear { get; set; } = DateTime.Now.Year;
        private List<int> Years { get; set; } = new() { 2023, 2024 };
        private List<ChartSeries> Series = new List<ChartSeries>()
        {
            new ChartSeries() { Name = "Series 1", Data = new double[] { 90, 79, 72, 69, 62, 62, 55, 65, 70 } },
            new ChartSeries() { Name = "Series 2", Data = new double[] { 10, 41, 35, 51, 49, 62, 69, 91, 148 } },
        };
        private string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }



    }
}
