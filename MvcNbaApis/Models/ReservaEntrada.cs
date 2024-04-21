using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNbaApis.Models
{
    public class ReservaEntrada
    {
        public int ReservaId { get; set; }
        public int UsuarioId { get; set; }
        public int PartidoId { get; set; }
        public int Asiento { get; set; }
    }
}

