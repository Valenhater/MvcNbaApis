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

        public async Task<IActionResult> VistaReservaEntradas()
        {
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
            var reserva = await this.service.ReservarEntradaAsync(usuarioid, partidoid, asiento);
            if (reserva != null)
            {
                // La reserva fue exitosa, redireccionar o retornar alguna vista de éxito
                return RedirectToAction("EntradasReservadas", new { id = reserva.ReservaId });
            }
            else
            {
                // La reserva no fue exitosa, redireccionar o retornar alguna vista de error
                return RedirectToAction("VistaReservaEntradas"); 
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
       
    }
}
