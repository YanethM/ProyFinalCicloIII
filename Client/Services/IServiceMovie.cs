using System.Security.AccessControl;
using proyFinal.Shared.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace proyFinal.Client.Services
{
    public interface IServiceMovie
    {
        List<Movie> GetMovies();
        Task<HttpResponseWrapper<object>> Post<T>(string url, T send);
        Task<HttpResponseWrapper<TResponse>> Post<T,TResponse>(string url, T send);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
    }
}