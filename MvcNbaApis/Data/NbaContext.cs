using Microsoft.EntityFrameworkCore;
using MvcNbaApis.Models;

namespace MvcNbaApis.Data
{
    public class NbaContext : DbContext
    {
        public NbaContext(DbContextOptions<NbaContext> options) : base(options) { }
        
        public DbSet<ModelVistaProximosPartidos> VistaProximosPartidos { get; set; }


    }
}
