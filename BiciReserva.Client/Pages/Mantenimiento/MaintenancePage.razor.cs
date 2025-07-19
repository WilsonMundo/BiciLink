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
        IEnumerable<BicycleDTO> bicycles;
        bool flagBicicletas = false;
        bool insert = false;

        void ResetForm()
        {
            mantenimiento = new MantenimientoDTO();
            StateHasChanged();
        }

        async Task OnSubmit(MantenimientoDTO mantenimiento)
        {
            insert = true;

            insert = true;
            var httpResponse = await Repository.Post<MantenimientoDTO>("api/v1/Maintenance", mantenimiento);
            if (httpResponse.Error)
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
        async Task GetBicycles()
        {
            
            try
            {
                var response = await Repository.Get<List<BicycleDTO>>("api/v1/Bicycle");

                if (response.Error)
                {
                    var body = await response.GetBody();
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
                    bicycles = response.Response ?? new List<BicycleDTO>();
                }
            }catch(Exception ex)
            {
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = ex.Message,
                    Duration = 5000
                });
            }
            finally
            {
                flagBicicletas = true;
            }
            
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
            await GetBicycles();
        }
    }
}
