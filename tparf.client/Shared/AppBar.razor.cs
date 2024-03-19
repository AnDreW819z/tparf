using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace tparf.client.Shared
{
    public partial class AppBar
        
    {

        [Parameter]
        public EventCallback OnEndBarToggled { get; set; }

        [Parameter]
        public EventCallback OnSidebarToggled { get; set; }
        
    }
}
