using Microsoft.AspNetCore.Components;

namespace InventoryManagementSystem.UI.Components
{
    public partial class Loading
    {
        [Parameter]
        public bool IsLoading { get; set; } = true;

    }
}
