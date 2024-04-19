using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcNbaApis.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
