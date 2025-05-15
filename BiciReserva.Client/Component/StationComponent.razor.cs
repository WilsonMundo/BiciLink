using Radzen;
using Radzen.Blazor;
using Shared.DTO;
using Shared.RepositoryShared;

namespace BiciReserva.Client.Component
{
    public partial class StationComponent
    {
        private EstacionDTO estacion = new();
        private RadzenTemplateForm<EstacionDTO> form;
        private bool procesando = false;


        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }
        void ResetForm()
        {
            estacion = new EstacionDTO();
           

        }
        private async Task OnSubmit(EstacionDTO estacion)
        {
            procesando = true;

            try
            {
                var httpResponse = await Repository.Post<EstacionDTO>("api/v1/Station", estacion);
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
            }
            finally
            {
                procesando = false;
            }
        }
    }
}
