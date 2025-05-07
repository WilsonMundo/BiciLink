using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.RepositoryShared
{
    public class Repository: IRepository
    {
        private readonly HttpClient httpClient;
        public Repository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        private JsonSerializerOptions OpcionesPorDefectoJSON =>
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var encodeUrl = Uri.EscapeDataString(url);
            var responseHTTP = await httpClient.GetAsync(encodeUrl);
            if (responseHTTP.IsSuccessStatusCode)
            {
                try
                {
                    if (typeof(T) == typeof(Stream))
                    {
                        //var stream = await responseHttp.Content.ReadAsStreamAsync();
                        return new HttpResponseWrapper<T>(default, false, responseHTTP);

                    }
                    else if (typeof(T) == typeof(string))
                    {
                        var responseString = await responseHTTP.Content.ReadAsStringAsync();
                        if (responseString != "")
                        {
                            var response = responseString;
                            return new HttpResponseWrapper<T>((T)(object)response, false, responseHTTP);
                        }
                        else
                        {
                            return new HttpResponseWrapper<T>(default, false, responseHTTP);
                        }
                    }
                    else
                    {
                        var responseString = await responseHTTP.Content.ReadAsStringAsync();
                        if (responseString != "")
                        {
                            var response = DeserializarRespuesta<T>(responseString, OpcionesPorDefectoJSON);
                            return new HttpResponseWrapper<T>(response, false, responseHTTP);
                        }
                        else
                        {
                            return new HttpResponseWrapper<T>(default, false, responseHTTP);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }


            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
        }



        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {

            var encodeUrl = Uri.EscapeDataString(url);
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(encodeUrl, enviarContent);
            return new HttpResponseWrapper<object>(responseHttp, !responseHttp.IsSuccessStatusCode, responseHttp);
        }


        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var encodeUrl = Uri.EscapeDataString(url);
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(encodeUrl, enviarContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                if (typeof(TResponse) == typeof(Stream))
                {
                    //var stream = await responseHttp.Content.ReadAsStreamAsync();
                    return new HttpResponseWrapper<TResponse>(default, true, responseHttp);

                }
                else
                {
                    var responseString = await responseHttp.Content.ReadAsStringAsync();
                    if ((responseString == "") || (responseString == "[]"))
                    {
                        return new HttpResponseWrapper<TResponse>(default, false, responseHttp);
                    }
                    else
                    {
                        var response = DeserializarRespuesta<TResponse>(responseString, OpcionesPorDefectoJSON);
                        return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
                    }
                }


            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var encodeUrl = Uri.EscapeDataString(url);
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(encodeUrl, enviarContent);
            return new HttpResponseWrapper<object>(responseHttp, !responseHttp.IsSuccessStatusCode, responseHttp);
        }
        public async Task<HttpResponseWrapper<object>> Post(string url)
        {
            var encodeUrl = Uri.EscapeDataString(url);
            var enviarJSON = JsonSerializer.Serialize("");
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(encodeUrl, enviarContent);
            return new HttpResponseWrapper<object>(responseHttp, !responseHttp.IsSuccessStatusCode, responseHttp);
        }
        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var encodeUrl = Uri.EscapeDataString(url);
            var responseHTTP = await httpClient.DeleteAsync(encodeUrl);
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        private T? DeserializarRespuesta<T>(string responseString, JsonSerializerOptions JsonSerializerOptions)
        {
         
            return JsonSerializer.Deserialize<T>(responseString, JsonSerializerOptions);

        }
    }
}
