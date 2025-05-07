using Microsoft.AspNetCore.Components;

namespace BiciReserva.Client.Component
{
    public partial class PanelMenuComponent
    {
        [Parameter]
        public EventCallback<bool> OnToggle { get; set; }
        private bool isMenuOpen = true;

        private async void ToggleMenu()
        {
            isMenuOpen = !isMenuOpen;
            await OnToggle.InvokeAsync(isMenuOpen);
        }
    }
}
