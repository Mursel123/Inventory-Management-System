using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.Components
{
    public partial class Loading
    {
        [Parameter]
        public List<bool> IsLoadingComponents { get; set; } = new();
        private bool ProgressClosed { get; set; } = false;
        protected override void OnInitialized()
        {
            ProgressClosed = IsLoadingComponents.Any(x => x == false);
        }
    }
}
