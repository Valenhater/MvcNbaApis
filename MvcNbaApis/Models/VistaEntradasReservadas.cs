using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNbaApis.Models
{
    public class VistaEntradasReservadas
    {
        public int ReservaId { get; set; }
        public int UsuarioId { get; set; }
        public int PartidoId { get; set; }
        public int Asiento { get; set; }
        public DateTime FechaPartido { get; set; }
        public int EquipoLocalId { get; set; }
        public int EquipoVisitanteId { get; set; }
        public decimal PrecioEntrada { get; set; }
        public int PlazasDisponibles { get; set; }
        public int IdCiudad { get; set; }
        public string ImagenPartido { get; set; }
    }
}
