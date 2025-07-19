using Radzen;
using Shared.Auth.DTO;
using Shared.Auth;
using Microsoft.AspNetCore.Components;
using Shared.RepositoryShared;

namespace BiciReserva.Client.Pages.Login
{
    public partial class LoginPage
    {
        private UserInfo userInfo = new UserInfo();
        private Boolean invalidDate { get; set; } = false;
        private bool Enviar { get; set; } = false;
        private string dataError { get; set; } = "Credenciales Invalidas";
       
        private async Task OnSubmit()
        {
            Enviar = !Enviar;
            var httpResponse = await repository.Post<UserInfo>("api/v1/Autenticacion/login", userInfo);
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
                Enviar = !Enviar;
            }           
            else if (httpResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                await loginService.Login();
                NavigationManager.NavigateTo("");
            }
            else
            {
                ShowNotification(new NotificationMessage
                {
                    Style = "display:flex;width:100%;justify-content: center;",
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = "Credenciales invalidas",
                    Duration = 50000
                });
                Enviar = !Enviar;

            }
        }
        private void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);

        }


    }
}
