using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNbaApis.Models
{
    public class Equipo
    {
        public int IdEquipo { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public DateTime Fundacion { get; set; }
        public string Imagen { get; set; }
        public string ImagenFondo { get; set; }
    }
}
