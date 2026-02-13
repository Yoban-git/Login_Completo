using PruebaLogin.Models.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaLogin.Models.User
{
    public class Usuarios2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "El Correo es obligatorio")]
        public string? Email { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; } = true;
        // Relación con Rol
        public int IdRol { get; set; }
        public Roles2 Rol { get; set; }
        
    }
}
