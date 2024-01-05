using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Domain.Entities.NotMapped
{
    //This entity is for the mudblazor component to visualize a chart
    [NotMapped]
    public class ChartSeries
    {
        public string Name { get; set; } = string.Empty;
        public double[] Data { get; set; } = Array.Empty<double>();
    }
}
