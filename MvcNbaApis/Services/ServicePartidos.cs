using MvcNbaApis.Models;
using System.Net.Http.Headers;

namespace MvcNbaApis.Services
{
    public class ServicePartidos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServicePartidos(IConfiguration configuration)
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
        public async Task<List<Partido>> GetPartidosAsync()
        {
            string request = "api/partidos/getpartidos";
            List<Partido> data = await this.CallApiAsync<List<Partido>>(request);
            return data;
        }
        public async Task<List<Equipo>> GetEquiposAsync()
        {
            string request = "api/partidos/getallequipos";
            List<Equipo> data = await this.CallApiAsync<List<Equipo>>(request);
            return data;
        }
        //RECUERDA CAMBIAR EL PARTIDOS POR EQUIPO ERROR
        public async Task<Equipo> FindEquipoAsync(int idEquipo)
        {
            string request = "api/partidos/" + idEquipo;
            Equipo data = await this.CallApiAsync<Equipo>(request);
            return data;
        }
    }
}
