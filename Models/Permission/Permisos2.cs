using PruebaLogin.Models.RolesPermisos;

namespace PruebaLogin.Models.Permisos
{
    public class Permisos2
    {
        public int IdPermiso { get; set; }
        public string NombrePermiso { get; set; }

        //Relacion muchos a muchoc con Roles
        public ICollection<RolPermiso> RolPermisos { get; set; }
    }
}
