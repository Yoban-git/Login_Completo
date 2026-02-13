using PruebaLogin.Models.Permisos;
using PruebaLogin.Models.Roles;

namespace PruebaLogin.Models.RolesPermisos
{
    public class RolPermiso
    { 
        public int IdRol { get; set; } 
        public Roles2 Rol { get; set; }

        public int IdPermiso { get; set; } 
        public Permisos2 Permiso { get; set; }
    }
}
