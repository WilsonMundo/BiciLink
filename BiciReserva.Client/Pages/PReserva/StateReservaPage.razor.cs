using Radzen.Blazor;
using Radzen;
using Shared.DTO;

namespace BiciReserva.Client.Pages.PReserva
{
    public partial class StateReservaPage
    {
        RadzenDataGrid<GStateGeneralDTO> estadoGrid;
        IEnumerable<GStateGeneralDTO> estados;
        List<GStateGeneralDTO> estadosToInsert = new();
        bool isLoading = false;
        bool insert = false;

        DataGridEditMode editMode = DataGridEditMode.Single;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetStates();
        }
        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }

        async Task GetStates()
        {
            var httpResponse = await Repository.Get<List<GStateGeneralDTO>>("api/v1/EstadoReserva");

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

        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }

        void Reset()
        {
            estadosToInsert.Clear();
        }

        void Reset(GStateGeneralDTO estado)
        {
            estadosToInsert.Remove(estado);
        }

        void EditRow(GStateGeneralDTO estado)
        {
            if (editMode == DataGridEditMode.Single)
                Reset();

            estadoGrid.EditRow(estado);
        }

        void CancelEdit(GStateGeneralDTO estado)
        {
            Reset(estado);
            estadoGrid.CancelEditRow(estado);
        }

        async Task SaveRow(GStateGeneralDTO estado)
        {
            await estadoGrid.UpdateRow(estado);
        }

        async Task DeleteRow(GStateGeneralDTO estado)
        {
            insert = true;
            var response = await Repository.Delete($"api/v1/EstadoReserva/{estado.idEstado}");

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
                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Exito",
                    Detail = "Eliminado con exito",
                    Duration = 5000
                });
                Reset(estado);
                await estadoGrid.Reload();
            }
            insert = false;


        }

        async Task InsertRow()
        {
            if (editMode == DataGridEditMode.Single)
                Reset();

            var nuevo = new GStateGeneralDTO();
            estadosToInsert.Add(nuevo);
            await estadoGrid.InsertRow(nuevo);
        }

        async Task InsertAfterRow(GStateGeneralDTO row)
        {
            if (editMode == DataGridEditMode.Single)
                Reset();

            var nuevo = new GStateGeneralDTO();
            estadosToInsert.Add(nuevo);
            await estadoGrid.InsertAfterRow(nuevo, row);
        }

        async Task OnCreateRow(GStateGeneralDTO estado)
        {
            insert = true;
            var state = new GEstadoDTO { Descripcion = estado.descripcion };

            var response = await Repository.Post<GEstadoDTO, GStateGeneralDTO>("api/v1/EstadoReserva", state);

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
                estado.idEstado = response.Response.idEstado;
                estado.descripcion = response.Response.descripcion;
                estadosToInsert.Remove(estado);
            }
            insert = false;

        }

        async Task OnUpdateRow(GStateGeneralDTO estado)
        {
            insert = true;
            var response = await Repository.Put<GStateGeneralDTO>($"api/v1/EstadoReserva", estado);

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
                insert = false;

                ShowNotification(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Exito",
                    Detail = "Actualizado con exito",
                    Duration = 5000
                });
                await SaveRow(estado);
            }

        }

        void OnRowRender(RowRenderEventArgs<GStateGeneralDTO> args)
        {
            // Personalización opcional por fila
        }
    }
}
