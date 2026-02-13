using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.User;
using PruebaLogin.Models.Roles;
using PruebaLogin.Models.Permisos;
using PruebaLogin.Models.RolesPermisos;
using PruebaLogin.Configurations;

namespace PruebaLogin.Models.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //este en el bueno
        public DbSet<Usuarios2> Usuarios2 { get; set; }
        public DbSet<Roles2> Roles { get; set; }
        public DbSet<Permisos2> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermiso { get; set; }
       
       
       
       
       
        public DbSet<Usuarioguid> Usuarioguid { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new PermisoConfiguration());
            modelBuilder.ApplyConfiguration(new RolPermisoConfiguration());
        }
    }
}
