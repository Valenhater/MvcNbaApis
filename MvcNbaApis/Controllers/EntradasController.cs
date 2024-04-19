using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> VistaReservaEntradas()
        {
            var entradas = await this.service.GetEntradasAsync();
            return View(entradas);
        }
        public async Task<IActionResult> ReservarEntrada(int id)
        {
            var entrada = await this.service.FindEntradaAsync(id);
            return View(entrada);
        }
        [HttpPost]
        public async Task<IActionResult> ReservarEntrada(int usuarioId, int partidoId, int asiento)
        {
            var reserva = await this.service.ReservarEntradaAsync(usuarioId, partidoId, asiento);
            if (reserva != null)
            {
                // La reserva fue exitosa, redireccionar o retornar alguna vista de éxito
                return RedirectToAction("PartidosFavoritos", new { id = reserva.ReservaId });
            }
            else
            {
                // La reserva no fue exitosa, redireccionar o retornar alguna vista de error
                return RedirectToAction("EntVistaReservaEntradasradas"); 
            }
        }

        public async Task<IActionResult> EliminarReserva(int id)
        {
            await this.service.DeleteEntradaAsync(id);
            // Redireccionar o retornar alguna vista indicando que la entrada fue eliminada
            return RedirectToAction("VistaReservaEntradas");
        }

        public async Task<IActionResult> PartidosFavoritos()
        {
            var entradasReservadas = await this.service.GetEntradasReservadasAsync();
            return View(entradasReservadas);
        }
    }
}
