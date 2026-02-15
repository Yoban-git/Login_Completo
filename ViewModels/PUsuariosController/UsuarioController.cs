using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.Data;
using PruebaLogin.Models.User;

namespace PruebaLogin.ViewModels.PUsuariosController
{
    public class ResultadoOperacion
    {
        public bool Exito {  get; set; }
        public string Error { get; set; }
    }
    public class UsuarioController
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }
        //Listar o Read
        public async Task<List<Usuarios2>> ObtenerTodosUsuariosAsyng()
        {
                return await _context.Usuarios2.ToListAsync();
        }

        //consultar un usuario por su user name
        public async Task<Usuarios2> ObtenerUsuarioPorIdAsyng(string _userName)
        {
            try
            {
                return await _context.Usuarios2.FirstOrDefaultAsync( u => u.UserName == _userName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"El usuario no existe o se registro  con otro nombre {ex.Message}");
                return null;
            }
        }
        //Cread
        public async Task<ResultadoOperacion> AgregarUsuariosAsyng(Usuarios2 usuarios)
        {
            try
            {
                var rolExiste = await _context.Roles.AnyAsync(r => r.IdRol == usuarios.IdRol);
                if (!rolExiste)
                {
                    return new ResultadoOperacion { Exito = false, Error = "El rol especificado no existe." };
                }
                
                usuarios.Id = Guid.NewGuid();
                // Hashear la contraseña antes de guardar
                usuarios.Password = BCrypt.Net.BCrypt.HashPassword(usuarios.Password);


                _context.Usuarios2.Add(usuarios);
                await _context.SaveChangesAsync();

                return new ResultadoOperacion { Exito = true };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion { Exito = false, Error = ex.Message };
            }
        }
        //actuilizar
        public async Task<ResultadoOperacion> ActualizarUsuarioASyng(Usuarios2 usuario)
        {
            try
            {
                // Verificar que el usuario existe
                var usuarioDb = await _context.Usuarios2.FindAsync(usuario.Id);

                if (usuarioDb == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Error = "Usuario no encontrado"
                    };
                }
                // Actualizar solo los campos que necesitas
                usuarioDb.Nombre = usuario.Nombre;
                usuarioDb.UserName = usuario.UserName;
                usuarioDb.Email = usuario.Email;
                usuarioDb.IdRol = usuario.IdRol;
                await _context.SaveChangesAsync();
                return new ResultadoOperacion { Exito = true };
            }
            catch(Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Error = $"Error inesperado: {ex.Message}"
                };
            }
        }


        //activar/desactivar cuenat usuario
        public async Task<ResultadoOperacion> CambiarEstadoUsuario(Guid id)
        {
            try
            {
                var usuario = await _context.Usuarios2.FindAsync(id);
                if (usuario == null)
                    return new ResultadoOperacion { Exito = false, Error = "Usuario no encontrado." };

                usuario.Activo = !usuario.Activo; // alterna el estado
                _context.Usuarios2.Update(usuario);
                await _context.SaveChangesAsync();
                return new ResultadoOperacion { Exito = true };
            }
            catch(Exception ex)
            {
                return new ResultadoOperacion { Exito = false, Error = ex.Message };
            }
        }


        //eliminar
        public async Task<ResultadoOperacion> EliminarUsuarioAsyng(Guid id)
        {
            try
            {
                var usuario = await _context.Usuarios2.FindAsync(id);
                if (usuario == null)
                    return new ResultadoOperacion { Exito = false, Error = "Usuario no encontrado." };
                _context.Usuarios2.Remove(usuario);
                await _context.SaveChangesAsync();
                return new ResultadoOperacion { Exito = true };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion { Exito = false, Error = ex.Message };
            }
        }

    }
}
