using Radzen;
using Radzen.Blazor;
using Shared.DTO;
using System.Collections.Generic;

namespace BiciReserva.Client.Component
{
    public partial class BicycleListComponent
    {
        RadzenDataGrid<BicycleDTO> bicycleGrid;
        IEnumerable<BicycleDTO> bicycles;
        IEnumerable<GStateGeneralDTO> estados;

        List<BicycleDTO> bicyclesToInsert = new();
        bool isLoading = false;
        bool insert = false;

        DataGridEditMode editMode = DataGridEditMode.Single;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetStates();
            await GetBicycles();            
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

        async Task ShowLoading()
        {
            isLoading = true;
            await Task.Yield();
            isLoading = false;
        }
        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }
        async Task GetBicycles()
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
        }

      

        void EditRow(BicycleDTO bike)
        {
            if (editMode == DataGridEditMode.Single)
                bicyclesToInsert.Clear();

            bicycleGrid.EditRow(bike);
        }

        void CancelEdit(BicycleDTO bike)
        {
            bicyclesToInsert.Remove(bike);
            bicycleGrid.CancelEditRow(bike);
        }

        async Task SaveRow(BicycleDTO bike)
        {
            await bicycleGrid.UpdateRow(bike);
        }

        async Task DeleteRow(BicycleDTO bike)
        {
            insert = true;
            // var response = await Repository.Delete($"api/v1/Bicycle/{bike.bicycleId}");
            insert = false;
            await bicycleGrid.Reload();
        }

        async Task InsertRow()
        {
            if (editMode == DataGridEditMode.Single)
                bicyclesToInsert.Clear();

            var nuevo = new BicycleDTO();
            bicyclesToInsert.Add(nuevo);
            await bicycleGrid.InsertRow(nuevo);
        }

        async Task InsertAfterRow(BicycleDTO row)
        {
            if (editMode == DataGridEditMode.Single)
                bicyclesToInsert.Clear();

            var nuevo = new BicycleDTO();
            bicyclesToInsert.Add(nuevo);
            await bicycleGrid.InsertAfterRow(nuevo, row);
        }

        async Task OnCreateRow(BicycleDTO bike)
        {
            insert = true;
            // var response = await Repository.Post<BicycleDTO, BicycleDTO>("api/v1/Bicycle", bike);
            insert = false;
            bicyclesToInsert.Remove(bike);
        }

        async Task OnUpdateRow(BicycleDTO bike)
        {
            insert = true;
            // var response = await Repository.Put<BicycleDTO>("api/v1/Bicycle", bike);
            insert = false;
            await SaveRow(bike);
        }

        void OnRowRender(RowRenderEventArgs<BicycleDTO> args)
        {
            // Personalización opcional
        }
    }
}
