using Radzen.Blazor;
using Radzen;
using Shared.DTO;

namespace BiciReserva.Client.Pages.Mantenimiento
{
    public partial class MaintenancePage
    {
        MantenimientoDTO mantenimiento = new();
        IEnumerable<GStateGeneralDTO> estados;
        RadzenTemplateForm<MantenimientoDTO> form;
        bool insert = false;

        void ResetForm()
        {
            mantenimiento = new MantenimientoDTO();
            StateHasChanged();
        }

        async Task OnSubmit(MantenimientoDTO mantenimiento)
        {
            insert = true;

            // Llamada a la API
            // var httpResponse = await Repository.Post<MantenimientoDTO>("api/v1/Mantenimiento", mantenimiento);

            // Simular éxito
            bool success = true;

            if (!success)
            {
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = "No se pudo registrar el mantenimiento.",
                    Duration = 5000
                });
            }
            else
            {
                ResetForm();
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Éxito",
                    Detail = "Mantenimiento registrado con éxito.",
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

            var httpResponse = await Repository.Get<List<GStateGeneralDTO>>("api/v1/StateMaintenance");

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
