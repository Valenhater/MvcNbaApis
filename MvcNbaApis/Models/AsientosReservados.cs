using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNbaApis.Models
{
    public class AsientoReservado
    {

        public int Id { get; set; }
        public int PartidoId { get; set; }
        public int Asiento { get; set; }
        public virtual Partido Partido { get; set; }
        public ModelVistaProximosPartidos ProximosPartidos { get; set; }
    }
}
