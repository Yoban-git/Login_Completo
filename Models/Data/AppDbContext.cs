using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.User;

namespace PruebaLogin.Models.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //este en el bueno
        public DbSet<Usuarios2> Usuarios2 { get; set; }
        public DbSet<Usuarioguid> Usuarioguid { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
