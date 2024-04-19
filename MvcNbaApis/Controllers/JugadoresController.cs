using Microsoft.AspNetCore.Mvc;
using MvcNbaApis.Models;
using MvcNbaApis.Services;

namespace MvcNbaApis.Controllers
{
    public class JugadoresController : Controller
    {
        private ServiceJugadores service;

        public JugadoresController(ServiceJugadores service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Jugadores()
        {
            List<Jugador> jugadores = await this.service.GetJugadoresAsync();
            var viewModel = new JugadoresViewModel
            {
                Jugadores = jugadores
            };
            return View(viewModel);
        }
        public async Task<IActionResult> DetalleJugador(int id)
        {
            Jugador jugador = await this.service.FindJugadorAsync(id);
            return View(jugador);
        }

    }
}
