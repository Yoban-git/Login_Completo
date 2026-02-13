using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.Data;
using PruebaLogin.Models.Roles;

namespace PruebaLogin.ViewModels.RolController
{
    public class ResultadoOperacion
    {
        public bool Exito { get; set; }
        public string Error { get; set; }
    }

    public class RolService
    {
        private readonly AppDbContext _context;
        public RolService(AppDbContext context)
        {
            _context = context;
        }
	
	 //Listar o Read
        public async Task<List<Roles2>> ObtenerTodosRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        //consultar un usuario por su user name
        public async Task<Roles2> ObtenerRolPorNombreAsync(string roleName)
        {
            try
            {
                return await _context.Roles.FirstOrDefaultAsync( r => r.NombreRol == roleName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"El rol no existe o se registro  con otro nombre {ex.Message}");
                return null;
            }
        }	


        //Agregar
        public async Task<ResultadoOperacion> AgregarRolAsync(Roles2 role)
        {
            try
            {
                var rolExistente = await _context.Roles.AnyAsync(r => r.NombreRol == role.NombreRol);
                if (rolExistente) 
                { 
                    return new ResultadoOperacion { Exito = false, Error = "El rol ya existe en la base de datos." }; 
                }

                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return new ResultadoOperacion { Exito = true };
            }
            catch(Exception ex)
            {
                return new ResultadoOperacion { Exito = false, Error = ex.Message };
            }
        }
	//Actualizar
	    public async Task<ResultadoOperacion> ActualizarRolAsync(Roles2 role)
	    {
            try
            {
                var rolDb = await _context.Roles.FindAsync(role.IdRol);
                if (rolDb == null)
                {
                    return new ResultadoOperacion { Exito = false, Error = "rol no encontrado" };
                }

                // Validar que el nuevo nombre no esté duplicado
                var existeNombre = await _context.Roles.AnyAsync(r => r.NombreRol == role.NombreRol && r.IdRol != role.IdRol);

                if (existeNombre)
                {
                    return new ResultadoOperacion { Exito = false, Error = "el rol ya existe" };
                }

                rolDb.NombreRol = role.NombreRol;
                await _context.SaveChangesAsync();
                return new ResultadoOperacion { Exito = true };
            }
            catch (Exception ex)
            {

                return new ResultadoOperacion { Exito = false, Error = $"Error inesperado: {ex.Message}" };
            }
	    }

        //eliminar
        public async Task<ResultadoOperacion> EliminarRolAsync(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                    return new ResultadoOperacion { Exito = false, Error = "Rol no encontrado." };

                _context.Roles.Remove(rol);
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
