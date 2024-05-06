using Microsoft.AspNetCore.Mvc;
using MvcNbaApis.Extensions;
using MvcNbaApis.Models;
using MvcNbaApis.Services;

namespace MvcNbaApis.Controllers
{
    public class EntradasController : Controller
    {
        private ServiceEntradas service;
        public EntradasController(ServiceEntradas service)
        {
            this.service = service;
        }

        public async Task<IActionResult> VistaReservaEntradas(int? idPartido)
        {
            if (idPartido != null)
            {
                List<int> partidosList = HttpContext.Session.GetObject<List<int>>("PARTIDOS") ?? new List<int>();
                partidosList.Add(idPartido.Value);
                HttpContext.Session.SetObject("PARTIDOS", partidosList);
                ViewData["MENSAJE"] = "Partido almacenado correctamente";
            }
            var entradas = await this.service.GetEntradasAsync();
            return View(entradas);
        }
        public async Task<IActionResult> ReservarEntrada(int partidoid)
        {
            var entrada = await this.service.FindEntradaAsync(partidoid);
            ViewData["PARTIDOID"] = partidoid;
            return View(entrada);
        }
        [HttpPost]
        public async Task<IActionResult> ReservarEntrada(int usuarioid, int partidoid, int asiento)
        {
            // Lógica para reservar la entrada
            var reserva = await this.service.ReservarEntradaAsync(usuarioid, partidoid, asiento);
            if (reserva != null)
            {
                // La reserva fue exitosa, redireccionar a la página de entradas reservadas
                return RedirectToAction("EntradasReservadas", new { usuarioid = usuarioid });
            }
            else
            {
                // La reserva no fue exitosa, redireccionar a la vista de reserva de entradas
                return RedirectToAction("VistaReservaEntradas", new { partidoid = partidoid });
            }
        }

        public async Task<IActionResult> EliminarReserva(int id)
        {
            await this.service.DeleteEntradaAsync(id);
            // Redireccionar o retornar alguna vista indicando que la entrada fue eliminada
            return RedirectToAction("EntradasReservadas");
        }

        public async Task<IActionResult> EntradasReservadas(int usuarioid)
        {
            var entradasReservadas = await this.service.GetEntradasReservadasAsync(usuarioid);
            return View(entradasReservadas);
        }
        public async Task<IActionResult> PartidosFavoritos(int? ideliminar)
        {
            List<int> idsPartidos = HttpContext.Session.GetObject<List<int>>("PARTIDOS");
            if (idsPartidos != null)
            {
                if (ideliminar != null)
                {
                    idsPartidos.Remove(ideliminar.Value);
                    HttpContext.Session.SetObject("PARTIDOS", idsPartidos);
                }
                List<ModelVistaProximosPartidos> partidos = await this.service.GetFavoritosAsync(idsPartidos);
                return View(partidos);
            }
            ViewData["MENSAJE"] = "No hay partidos almacenados";
            return View();
        }
    }
}
