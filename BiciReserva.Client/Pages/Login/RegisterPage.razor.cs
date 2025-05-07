using BiciReserva.Client.Component;
using Radzen;
using Radzen.Blazor;
using Shared.Auth.DTO;

namespace BiciReserva.Client.Pages.Login
{
    public partial class RegisterPage
    {
        private UserRegister registerUser = new();
        private bool Guardando;
        private async Task OnSubmit()
        {
            var httpResponse = await repository.Post<UserRegister>("api/v1/Autenticacion/register", registerUser);
            if (httpResponse.Error)
            {
                var body = await httpResponse.GetBody();
                ShowNotification(new NotificationMessage
                {
                    Style = "display:flex;width:100%;justify-content: center;",
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = body,
                    Duration = 50000
                });
                Guardando = !Guardando;
            }
            else if (httpResponse.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                await ShowInlineDialog();
            }
            else if (httpResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                Guardando = !Guardando;

                ShowNotification(new NotificationMessage
                {
                    Style = "display:flex;width:100%;justify-content: center;",
                    Severity = NotificationSeverity.Success,
                    Summary = "Registro",
                    Detail = "Registro Correcto Inicie Sesion",
                    Duration = 5000
                });
                NavigationManager.NavigateTo("/login");
            }
        }
        private void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);

        }
        

        async Task ShowInlineDialog()
        {
            var result = await DialogService.OpenAsync<DialogComponent>("¿Ya tienes una cuenta de BiciLink?",
                parameters: new Dictionary<string, object>() { { nameof(DialogComponent.DialogMessage), "Si tiene una cuenta, podemos ayudarle a encontrarla e iniciar sesión. " } },
                       options: new DialogOptions() { Style = "position:initial;" });
            if (result == true)
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
