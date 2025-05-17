using Radzen.Blazor;
using Radzen;
using Shared.DTO;
using Shared.RepositoryShared;

namespace BiciReserva.Client.Component.CStation
{
    public partial class ListStationComponent
    {
        RadzenDataGrid<EstacionDTO> estacionGrid;
        IEnumerable<EstacionDTO> estaciones;
        List<EstacionDTO> estacionesToInsert = new();

        bool isLoading = false;
        bool insert = false;

        DataGridEditMode editMode = DataGridEditMode.Single;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetEstaciones();
        }

        async Task GetEstaciones()
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
        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }

        async Task ShowLoading()
        {
            isLoading = true;
            await Task.Yield();
            isLoading = false;
        }

     
        void EditRow(EstacionDTO item)
        {
            if (editMode == DataGridEditMode.Single)
                estacionesToInsert.Clear();

            estacionGrid.EditRow(item);
        }

        void CancelEdit(EstacionDTO item)
        {
            estacionesToInsert.Remove(item);
            estacionGrid.CancelEditRow(item);
        }

        async Task SaveRow(EstacionDTO item)
        {
            await estacionGrid.UpdateRow(item);
        }

        async Task DeleteRow(EstacionDTO item)
        {
            insert = true;
            // await Repository.Delete($"api/v1/Estacion/{item.estacionId}");
            insert = false;
            await estacionGrid.Reload();
        }

        async Task InsertRow()
        {
            if (editMode == DataGridEditMode.Single)
                estacionesToInsert.Clear();

            var nuevo = new EstacionDTO();
            estacionesToInsert.Add(nuevo);
            await estacionGrid.InsertRow(nuevo);
        }

        async Task InsertAfterRow(EstacionDTO row)
        {
            if (editMode == DataGridEditMode.Single)
                estacionesToInsert.Clear();

            var nuevo = new EstacionDTO();
            estacionesToInsert.Add(nuevo);
            await estacionGrid.InsertAfterRow(nuevo, row);
        }

        async Task OnCreateRow(EstacionDTO item)
        {
            insert = true;
            // await Repository.Post<EstacionDTO, EstacionDTO>("api/v1/Estacion", item);
            insert = false;
            estacionesToInsert.Remove(item);
        }

        async Task OnUpdateRow(EstacionDTO item)
        {
            insert = true;
            // await Repository.Put<EstacionDTO>("api/v1/Estacion", item);
            insert = false;
            await SaveRow(item);
        }
    }
}
