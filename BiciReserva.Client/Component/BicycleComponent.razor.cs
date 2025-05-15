using Radzen.Blazor;
using Radzen;
using Shared.DTO;
using Shared.RepositoryShared;

namespace BiciReserva.Client.Component
{
    public partial class BicycleComponent
    {
        IEnumerable<GStateGeneralDTO> estados;
        RadzenTemplateForm<BicycleDTO> form;
        bool insert = false;
        void ResetForm()
        {
            bicycle = new BicycleDTO();
            StateHasChanged();

        }
        async Task OnSubmit(BicycleDTO bicycle)
        {
            insert = true;
            var httpResponse = await Repository.Post<BicycleDTO>("api/v1/Bicycle", bicycle);
            if (httpResponse.Error)
            {
                var body = await httpResponse.GetBody();
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = body,
                    Duration = 5000
                });
            }
            else
            {
                ResetForm();
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Exito",
                    Detail = "Registrada con Exito",
                    Duration = 5000
                });


            }
            insert = false;
        }
        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }
        async Task GetStates()
        {
            var httpResponse = await Repository.Get<List<GStateGeneralDTO>>("api/v1/StateBicycle");

            if (httpResponse.Error)
            {
                var body = await httpResponse.GetBody();
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = body,
                    Duration = 5000
                });
            }
            else
            {
                estados = httpResponse.Response ?? new List<GStateGeneralDTO>();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetStates();
        }
    }
}
