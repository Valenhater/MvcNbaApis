using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MvcNbaApis.Models;

namespace MvcNbaApis.Services
{
    public class ServiceUsuarios
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceUsuarios(IConfiguration configuration)
        {
            header = new MediaTypeWithQualityHeaderValue("application/json");
            UrlApi = configuration.GetValue<string>("ApiUrls:ApiNba");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
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

        public async Task<bool> LoginAsync(LoginModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.PostAsJsonAsync("api/auth/login", model);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.PostAsJsonAsync("api/auth/register", model);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<Usuario> VerPerfilAsync(string username)
        {
            string request = $"api/auth/{username}";
            return await CallApiAsync<Usuario>(request);
        }

        public async Task<bool> EditarPerfilAsync(string username, Usuario model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.PutAsJsonAsync($"api/auth/{username}", model);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.DeleteAsync($"api/auth/{id}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}
