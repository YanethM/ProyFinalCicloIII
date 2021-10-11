using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using proyFinal.Shared.Entity;

namespace proyFinal.Client.Services
{
    public class ServiceMovie : IServiceMovie
    {
private readonly HttpClient httpClient;

        public ServiceMovie(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        private JsonSerializerOptions OpcionesPorDefectoJSON =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

   
/* METODOS CREAR */
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }
/* Los siguientes dos métodos nos permitiran obtener el Id de la pelicula que acabo de crear */
        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }

      public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }


/* METODO CONSULTAR */
     public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);
            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<T>(responseHTTP, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(response, false, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
        }
        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        

        public List<Movie> GetMovies(){
            return new List<Movie>(){
                new Movie(){Id =1,Name="Movie 1", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie1.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =2,Name="Movie 2", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie2.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =3,Name="Movie 3", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie3.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =4,Name="Movie 4", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie4.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =5,Name="Movie 5", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie5.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =6,Name="Movie 6", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie6.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =7,Name="Movie 7", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie7.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =8,Name="Movie 8", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie8.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =9,Name="Movie 9", Sinopsis="Trama de la movie...", Poster="/Images/Movies/Movie9.jpg", Premier= new DateTime(2021,10,10)},
                new Movie(){Id =10,Name="Movie 10", Sinopsis="Trama de la movie...",Poster="/Images/Movies/Movie10.jpg", Premier= new DateTime(2021,10,10)}
            };
        }  
    }
}