using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RepositoryShared
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar);
        Task<HttpResponseWrapper<object>> Delete(string url);
        Task<HttpResponseWrapper<object>> Post(string url);

    }
}
