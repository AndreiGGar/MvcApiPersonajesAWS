using MvcApiPersonajesAWS.Models;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;

namespace MvcApiPersonajesAWS.Services
{

    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> TestApiAsync()
        {
            string request = "/api/Personajes";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) =>
            {
                return true;
            };
            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri(this.UrlApi);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(this.Header);
            HttpResponseMessage response = await client.GetAsync(request);
            return "Respuesta: " + response.StatusCode;
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) =>
                {
                    return true;
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);
                    HttpResponseMessage response = await client.GetAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        T data = await response.Content.ReadAsAsync<T>();
                        return data;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
        }

        private async Task<HttpStatusCode> PostApiAsync<T>(string request, T objeto)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);

                    string json = JsonConvert.SerializeObject(objeto);

                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(request, content);
                    return response.StatusCode;
                }
            }
        }

        private async Task<HttpStatusCode> PutApiAsync<T>(string request, T objeto)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);

                    string json = JsonConvert.SerializeObject(objeto);

                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(request, content);
                    return response.StatusCode;
                }
            }
        }

        private async Task<HttpStatusCode> DeleteApiAsync<T>(string request)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicy) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);
                    HttpResponseMessage response = await client.DeleteAsync(request);
                    return response.StatusCode;
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/Personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> GetPersonajeByIdAsync(int id)
        {
            string request = "/api/Personajes/" + id;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task CreatePersonajeAsync(string nombre, string imagen)
        {
            string request = "/api/Personajes/create/" + nombre + "/" + imagen;
            HttpStatusCode response = await this.PostApiAsync<string>(request, null);
        }

        public async Task UpdatePersonajeAsync(int id, string nombre, string imagen)
        {
            string request = "/api/Personajes/update/" + id + "/" + nombre + "/" + imagen;
            HttpStatusCode response = await this.PutApiAsync<string>(request, null);
        }

        public async Task DeletePersonajeAsync(int id)
        {
            string request = "/api/Personajes/delete/" + id;
            HttpStatusCode response = await this.DeleteApiAsync<string>(request);
        }
    }
}
