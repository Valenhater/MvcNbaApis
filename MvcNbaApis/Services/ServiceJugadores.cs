using MvcNbaApis.Models;
using System.Net.Http.Headers;

namespace MvcNbaApis.Services
{
    public class ServiceJugadores
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceJugadores(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiNba");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
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
        public async Task<List<Jugador>> GetJugadoresAsync()
        {
            string request = "api/jugadores";
            List<Jugador> data = await this.CallApiAsync<List<Jugador>>(request);
            return data;
        }
        public async Task<Jugador> FindJugadorAsync(int idJugador)
        {
            string request = "api/jugadores/" + idJugador;
            Jugador data = await this.CallApiAsync<Jugador>(request);
            return data;
        }
    }
}
