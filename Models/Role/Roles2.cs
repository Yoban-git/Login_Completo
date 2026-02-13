using PruebaLogin.Models.RolesPermisos;
using PruebaLogin.Models.User;
using System.ComponentModel.DataAnnotations;

namespace PruebaLogin.Models.Roles
{
    public class Roles2
    {
        [Key]
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        //Relacion con usuairos
        public ICollection<Usuarios2> Usuarios { get; set; } = new List<Usuarios2>();
        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
        
    }
}
