using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcNbaApis.Models
{
    public class ModelVistaProximosPartidos
    {
        [Key]
        [Column("PARTIDOID")]
        public int IdPartido { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public string CiudadLocal { get; set; }
        public string CiudadVisitante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioEntrada { get; set; }
        public int PlazasDisponibles { get; set; }
        public string ImagenPartido { get; set; }
    }
}
