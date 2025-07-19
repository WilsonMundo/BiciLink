using Radzen;
using Shared.DTO;
using System.Security.Claims;

namespace BiciReserva.Client.Pages.PBicycle
{
    public partial class HistorialPage
    {
        List<ReservaDTO>? reservasFiltradas;
        string? mensajeError;
        IEnumerable<GStateGeneralDTO> estados;
        IEnumerable<BicycleDTO> bicycles;
        IEnumerable<EstacionDTO> estaciones;
        IEnumerable<EstacionDTO> estacionesOrigenFiltradas;
        IEnumerable<EstacionDTO> estacionesDestinoFiltradas;
        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }

        async Task GetEstados()
        {
            var response = await Repository.Get<List<GStateGeneralDTO>>("api/v1/EstadoReserva");

            if (response.Error)
            {
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = await response.GetBody(),
                    Duration = 5000
                });
            }
            else
            {
                estados = response.Response ?? new List<GStateGeneralDTO>();
            }
        }
        async Task GetEstaciones()
        {
            var response = await Repository.Get<List<EstacionDTO>>("api/v1/Station");

            if (response.Error)
            {
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = await response.GetBody(),
                    Duration = 5000
                });
            }
            else
            {
                estaciones = response.Response ?? new List<EstacionDTO>();
                estacionesOrigenFiltradas = estaciones;
                estacionesDestinoFiltradas = estaciones;
            }
        }
        async Task GetBicycles()
        {
            var response = await Repository.Get<List<BicycleDTO>>("api/v1/Bicycle");

            if (response.Error)
            {
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = await response.GetBody(),
                    Duration = 5000
                });
            }
            else
            {
                bicycles = response.Response ?? new List<BicycleDTO>();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await GetBicycles();
                await GetEstaciones();
                await GetEstados();
                
                var response = await Repository.Get<List<ReservaDTO>>("api/v1/Reserva");

                if (!response.Error)
                {
                    reservasFiltradas = response.Response ?? new();

                }
                else
                {
                    mensajeError = await response.GetBody();
                }

            }
            catch (Exception ex)
            {
                mensajeError = $"Error al cargar el historial: {ex.Message}";
            }
        }
    }
}
