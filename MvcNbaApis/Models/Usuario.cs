using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNbaApis.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public byte[] Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public int Rol { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
    }
}
