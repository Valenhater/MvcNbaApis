using Microsoft.EntityFrameworkCore;
using MvcNbaApis.Data;
using MvcNbaApis.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcNbaApis.Services
{
    public class ServiceEntradas
    {
        private NbaContext context;
        private IHttpContextAccessor httpContextAccessor;
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceEntradas(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, NbaContext context)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiNba");
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
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
        public async Task<List<ModelVistaProximosPartidos>> GetEntradasAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Recuperar el token JWT de la sesión
                string token = this.httpContextAccessor.HttpContext.Session.GetString("TOKEN");
                // Añadir el token JWT en el encabezado Authorization
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            string request = "api/entradas/getentradas";
            List<ModelVistaProximosPartidos> data = await this.CallApiAsync<List<ModelVistaProximosPartidos>>(request);
            return data;
        }
        public async Task<ModelVistaProximosPartidos> FindEntradaAsync(int idEntrada)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Recuperar el token JWT de la sesión
                string token = this.httpContextAccessor.HttpContext.Session.GetString("TOKEN");
                // Añadir el token JWT en el encabezado Authorization
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            string request = "api/entradas/getentrada/" + idEntrada;
            ModelVistaProximosPartidos data = await this.CallApiAsync<ModelVistaProximosPartidos>(request);
            return data;
        }
        public async Task DeleteEntradaAsync(int idEntrada)
        {
            using (HttpClient client = new HttpClient())
            {
                // Recuperar el token JWT de la sesión
                string token = this.httpContextAccessor.HttpContext.Session.GetString("TOKEN");
                // Añadir el token JWT en el encabezado Authorization
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                string request = "api/entradas/" + idEntrada;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);

            }
        }
        public async Task<ReservaEntrada> ReservarEntradaAsync(int usuarioId, int partidoId, int asiento)
        {
            // Crear un objeto que represente los datos a enviar en la solicitud POST
            var reservaData = new
            {
                UsuarioId = usuarioId,
                PartidoId = partidoId,
                Asiento = asiento
            };

            // Convertir el objeto a JSON
            var jsonContent = JsonConvert.SerializeObject(reservaData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST a la API
            using (HttpClient client = new HttpClient())
            {
                // Establecer la URL de la API
                string requestUrl = "api/entradas";

                // Configurar el encabezado
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                // Enviar la solicitud POST
                HttpResponseMessage response = await client.PostAsync(requestUrl, content);

                // Recuperar el token JWT de la sesión
                string token = this.httpContextAccessor.HttpContext.Session.GetString("TOKEN");
                // Añadir el token JWT en el encabezado Authorization
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar la respuesta JSON
                    var responseData = await response.Content.ReadAsStringAsync();
                    var reserva = JsonConvert.DeserializeObject<ReservaEntrada>(responseData);

                    // Retornar la reserva creada
                    return reserva;
                }
                else
                {
                    // En caso de que la solicitud no sea exitosa, lanzar una excepción o retornar null
                    return null;
                }
            }
        }
        public async Task<List<VistaEntradasReservadas>> GetEntradasReservadasAsync(int idusuario)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Recuperar el token JWT de la sesión
                string token = this.httpContextAccessor.HttpContext.Session.GetString("TOKEN");
                // Añadir el token JWT en el encabezado Authorization
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            string request = "api/entradas/getentradasreservadas/" + idusuario;
            List<VistaEntradasReservadas> data = await this.CallApiAsync<List<VistaEntradasReservadas>>(request);
            return data;
        }
        public async Task<List<ModelVistaProximosPartidos>> GetFavoritosAsync(List<int> ids)
        {
            var consulta = from datos in this.context.VistaProximosPartidos where ids.Contains(datos.IdPartido) select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }
    }
}