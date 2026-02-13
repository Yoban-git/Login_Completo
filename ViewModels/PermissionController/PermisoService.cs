using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.Data;
using PruebaLogin.Models.Permisos;
namespace PruebaLogin.ViewModels.PermissionController
{
    public class ResultadoOperacion
    {
        public bool Exito {  get; set; }
        public string Error { get; set; }
    }

    public class PermisoService
    {
        private readonly AppDbContext _context;

        public PermisoService(AppDbContext context)
        {
            _context = context;
        }

        //Listar o Read
        public async Task<List<Permisos2>> ObtenerTodosPermisosAsync()
        {
            return await _context.Permisos.ToListAsync();
        }
        //consultar un usuario por su user name
        public async Task<Permisos2> ObtenerPermisoAsyncPorNombre(string permisoName)
        {
            try
            {
                return await _context.Permisos.FirstOrDefaultAsync( p => p.NombrePermiso == permisoName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"El permiso no existe o se registro  con otro nombre {ex.Message}");
                return null;
            }
        }
        //Cread
        public async Task<ResultadoOperacion> AgregarPermisoAsync(Permisos2 permiso)
        {
            try
            {
		        var existe = await _context.Permisos.AnyAsync(p => p.NombrePermiso == permiso.NombrePermiso);
		        if(existe)
		        {
                    return new ResultadoOperacion { Exito = false, Error = "El permiso ya existe en la base de datos." };
                }

                _context.Permisos.Add(permiso);
                await _context.SaveChangesAsync();
                return new ResultadoOperacion { Exito = true };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion { Exito = false, Error = ex.Message };
            }
        }
        //actuilizar
        public async Task<ResultadoOperacion> ActualizarPermisoAsync(Permisos2 permiso)
        {
            try
            {
                // Verificar que el usuario existe
                var permisoDb = await _context.Permisos.FindAsync(permiso.IdPermiso);

                if (permisoDb == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Error = "Permiso no encontrado"
                    };
                }
                //verificar que el nuevo nombre no se repitra
                var existeNombre = await _context.Permisos.AnyAsync(p => p.NombrePermiso == permiso.NombrePermiso && p.IdPermiso != permiso.IdPermiso);
                
                if(existeNombre)
                {
                    return new ResultadoOperacion { Exito = false, Error = "Ya existe otro permiso con ese nombre." };
                }
                // Actualizar solo los campos que necesitas
                permisoDb.NombrePermiso = permiso.NombrePermiso;
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

        //eliminar
        public async Task<ResultadoOperacion> EliminarPermisoAsync(int id)
        {
            try
            {
                var permiso = await _context.Permisos.FindAsync(id);
                if (permiso == null)
                    return new ResultadoOperacion { Exito = false, Error = "Permimso no encontrado." };

                _context.Permisos.Remove(permiso);
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
