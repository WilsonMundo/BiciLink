using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Shared.Auth.DTO;
using Shared.RepositoryShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Auth
{
    public class ProveedorAutenticacionJWT : AuthenticationStateProvider, ILoginService
    {
        private readonly IRepository _Repository;
        
        private readonly HttpClient httpClient;
        private readonly IJSRuntime _js;
        private AuthenticationState Anonimo =>
        new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        public ProveedorAutenticacionJWT(IJSRuntime js, IRepository repository, HttpClient httpClient)
        {            
            this.httpClient = httpClient;
            this._Repository = repository;
            this._js = js;
        }
        public async Task<UserInfoModel> ObtenerInfoModel()
        {
            try
            {
                UserInfoModel infoModel = new UserInfoModel();
                var httpResponse = await _Repository.Get<UserInfoModel>("api/v1/Autenticacion/auth/verificar");
                if (httpResponse.Error)
                {
                    return infoModel;
                }
                else
                {
                    infoModel = httpResponse.Response;
                }
                return infoModel;
            }
            catch
            {
                return new UserInfoModel();
            }

        }
        private AuthenticationState ConstruirAuthenticationState(UserInfoModel userInfo)
        {

            var identity = userInfo != null
                ? new ClaimsIdentity(ParseClaimsFromUsuarioInfo(userInfo), "ServerAuthentication")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));

        }
        private IEnumerable<Claim> ParseClaimsFromUsuarioInfo(UserInfoModel usuarioInfo)
        {
            var claims = new List<Claim>();
            if (usuarioInfo.Claims != null && usuarioInfo.Claims.Count() > 0)
            {
                foreach (UserInfoModel.ClaimModel item in usuarioInfo.Claims)
                {
                    var claim = new Claim(item.Type, item.Value);
                    claims.Add(claim);
                }
            }


            return claims;
        }
        private async Task Limpiar()
        {

        }
        public async Task LoginOut()
        {
            try
            {
                object nuevo = new object();
                var httpResponse = await _Repository.Post<object>("api/v1/Autenticacion/logout", nuevo);
                if (httpResponse.Error)
                {
                    NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
                }
                else if (httpResponse.HttpResponseMessage.IsSuccessStatusCode)
                {
                    NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
                }
                else
                {
                    NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
            }
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            UserInfoModel infoModel = await ObtenerInfoModel();
         
            if (infoModel.Name == null && (infoModel.Claims == null || infoModel.Claims.Count() == 0))
            {                
                return Anonimo;
            }
            return ConstruirAuthenticationState(infoModel);


        }

        public async Task Login()
        {
            UserInfoModel userToken = await ObtenerInfoModel();
            var authState = ConstruirAuthenticationState(userToken);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task LogOut()
        {
            await LoginOut();
            await Limpiar();
        }
    }
}
