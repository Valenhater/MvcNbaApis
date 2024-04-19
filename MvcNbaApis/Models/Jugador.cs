using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNbaApis.Models
{
    public class Jugador
    {
        public int IdJugador { get; set; }
        public string Nombre { get; set; }
        public int EquipoId { get; set; }
        public string Posicion { get; set; }
        public decimal PuntosPorPartido { get; set; }
        public decimal RebotesPorPartido { get; set; }
        public decimal AsistenciasPorPartido { get; set; }
        public string Imagen { get; set; }
    }
}
