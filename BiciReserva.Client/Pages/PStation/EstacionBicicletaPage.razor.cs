using Radzen;
using Shared.DTO;

namespace BiciReserva.Client.Pages.PStation
{
    public partial class EstacionBicicletaPage
    {
        long? estacionSeleccionadaId;

        IEnumerable<EstacionDTO> estaciones;
        IEnumerable<BicycleDTO> todasLasBicicletas;
        IEnumerable<BicycleDTO> bicicletasAsignadas ;
        IEnumerable<BicycleDTO> bicicletasDisponibles ;

        bool guardando = false;

        protected override async Task OnInitializedAsync()
        {
            await CargarEstaciones();
            await CargarBicicletas();
        }
        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }
        async Task CargarEstaciones()
        {
            var response = await Repository.Get<List<EstacionDTO>>("api/v1/Station");

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
                estaciones = response.Response ?? new List<EstacionDTO>();
            }
        }

        async Task CargarBicicletas()
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
                todasLasBicicletas = response.Response ?? new List<BicycleDTO>();
            }
        }

        async Task OnEstacionSeleccionada(object value)
        {
            if (value is not long estacionId) return;

            //var response = await Repository.Get<List<EstacionBicicleta>>($"api/v1/Station/{estacionId}");
            //if (response.Error)
            //{
            //    var body = await response.GetBody();
            //    ShowNotification(new NotificationMessage
            //    {
            //        Severity = NotificationSeverity.Error,
            //        Summary = "Error",
            //        Detail = body,
            //        Duration = 5000
            //    });
            //}
            //else
            //{                
            //    var asignadasIds = response.Response?.Select(e => e.bicycleId).ToHashSet() ?? new();
            //}
            // 

            // bicicletasAsignadas = todasLasBicicletas.Where(b => b.bicycleId != null && asignadasIds.Contains(b.bicycleId.Value)).ToList();
            // bicicletasDisponibles = todasLasBicicletas.Where(b => b.bicycleId != null && !asignadasIds.Contains(b.bicycleId.Value)).ToList();

            // Mock temporal (para pruebas)
            bicicletasAsignadas = todasLasBicicletas.Take(2).ToList();
            bicicletasDisponibles = todasLasBicicletas.Skip(2).ToList();
        }

        async Task GuardarAsignaciones()
        {
            if (estacionSeleccionadaId == null)
                return;

            guardando = true;

            var relaciones = bicicletasAsignadas
                .Where(b => b.bicycleId != null)
                .Select(b => new EstacionBicicleta
                {
                    estacionId = estacionSeleccionadaId.Value,
                    bicycleId = b.bicycleId!.Value
                }).ToList();

            // await Repository.Post<List<EstacionBicicleta>, bool>("api/v1/Estacion/asignar-bicicletas", relaciones);

            guardando = false;

            ShowNotification(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Asignación exitosa",
                Detail = "Las bicicletas fueron asignadas correctamente",
                Duration = 4000
            });
        }


    }
}
