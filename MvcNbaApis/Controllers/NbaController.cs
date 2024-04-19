using Microsoft.AspNetCore.Mvc;
using MvcNbaApis.Models;
using MvcNbaApis.Services;

namespace MvcNbaApis.Controllers
{
    public class NbaController : Controller
    {
        private ServicePartidos service;
        public NbaController(ServicePartidos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> PartidosJugados()
        {
            List<Partido> partidos = await this.service.GetPartidosAsync();
            return View(partidos);
        }
        public async Task<IActionResult> Index()
        {
            List<Equipo> equipos = await this.service.GetEquiposAsync();
            return View(equipos);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            Equipo equipo = await this.service.FindEquipoAsync(id);
            return View(equipo);
        }
    }
}
