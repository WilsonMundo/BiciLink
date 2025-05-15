using Radzen;
using Radzen.Blazor;
using Shared.DTO;

namespace BiciReserva.Client.Pages.PStation
{
    public partial class StationPage
    {
        private int selectChangeStepsItem { get; set; } = 0;

        private void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);

        }
        private void OnSelectedIndexChanged(int newIndex)
        {
            selectChangeStepsItem = newIndex;
        }
    }
}
