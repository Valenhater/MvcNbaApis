using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNbaApis.Models
{
    public class ProximoPartido
    {
        public int IdPartido { get; set; }
        public int EquipoLocalId { get; set; }
        public int EquipoVisitanteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioEntrada { get; set; }
        public int PlazasDisponible { get; set; }
        public int IdCiudad { get; set; }
        public string ImagenPartido { get; set; }
    }
}
