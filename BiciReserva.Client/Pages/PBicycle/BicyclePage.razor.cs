using Radzen;
using Radzen.Blazor;
using Shared.DTO;
using Shared.RepositoryShared;

namespace BiciReserva.Client.Pages.PBicycle
{
    public partial class BicyclePage
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
