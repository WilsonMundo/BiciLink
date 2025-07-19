using Radzen.Blazor;
using Radzen;
using Shared.DTO;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace BiciReserva.Client.Pages.PReserva
{
    public partial class ReservationPage
    {
        ReservaDTO reserva = new();
        IEnumerable<GStateGeneralDTO> estados;
        IEnumerable<BicycleDTO> bicycles;
        IEnumerable<EstacionDTO> estaciones;
        IEnumerable<EstacionDTO> estacionesOrigenFiltradas;
        IEnumerable<EstacionDTO> estacionesDestinoFiltradas;

        RadzenTemplateForm<ReservaDTO> form;
        bool insertando = false;
        bool flagCarga = false;
        BicycleDTO? bicicletaSeleccionada;

        void OnBicicletaChanged(object value)
        {
            if (value is long bicicletaId)
            {
                bicicletaSeleccionada = bicycles.FirstOrDefault(x => x.bicycleId == bicicletaId);
            }
        }
        void OnChangeEstacionOrigen(object value)
        {
            if (value is long selectedId)
            {
                reserva.EstacionOrigenId = selectedId;
                estacionesDestinoFiltradas = estaciones.Where(x => x.estacionId != selectedId).ToList();

                
                if (reserva.EstacionDestinoId == selectedId)
                {
                    reserva.EstacionDestinoId = 0;
                }
            }
        }

        void OnChangeEstacionDestino(object value)
        {
            if (value is long selectedId)
            {
                reserva.EstacionDestinoId = selectedId;
                estacionesOrigenFiltradas = estaciones.Where(x => x.estacionId != selectedId).ToList();

                
                if (reserva.EstacionOrigenId == selectedId)
                {
                    reserva.EstacionOrigenId = 0;
                }
            }
        }
        
        async Task OnSubmit(ReservaDTO model)
        {
            insertando = true;

            var httpResponse = await Repository.Post<ReservaDTO, ReservaDTO>("api/v1/Reserva", model);

            if (httpResponse.Error)
            {
                var msg = await httpResponse.GetBody();
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = msg,
                    Duration = 5000
                });
            }
            else
            {
                var codigo = httpResponse.Response.CodigoDesbloqueo?? "N/A";
                reserva = new();
                ShowNotificationWithCustomContent(codigo);
            }

            insertando = false;
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

        async Task GetEstados()
        {
            var response = await Repository.Get<List<GStateGeneralDTO>>("api/v1/StateReserva");

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

        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetBicycles();
            await GetEstaciones();
         //   await GetEstados();
            flagCarga = true;
        }
    }
}
