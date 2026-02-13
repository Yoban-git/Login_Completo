using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.Data;
using PruebaLogin.Models.RolesPermisos;

namespace PruebaLogin.ViewModels.RPController
{
    public class ResultadoOperacion
    {
        public bool Exito { get; set; }
        public string Error { get; set; }
    }
    public class RolPermisoService
    {
        private readonly AppDbContext _context;

        public RolPermisoService(AppDbContext context)
        {
            _context = context;
        }
        // Listar todas las relaciones Rol-Permiso
        public async Task<List<RolPermiso>> ObtenerTodasRelacionesAsync()
        {
            return await _context.RolPermiso
                .Include(rp => rp.Rol)       // Incluye datos del Rol
                .Include(rp => rp.Permiso)   // Incluye datos del Permiso
                .ToListAsync();
        }

        // Listar todos los permisos asignados a un rol
        public async Task<List<RolPermiso>> ObtenerPermisosPorRolAsync(int idRol)
        {
            return await _context.RolPermiso
                .Include(rp => rp.Permiso)
                .Where(rp => rp.IdRol == idRol)
                .ToListAsync();
        }

        // Asignar un permiso a un rol
        public async Task<ResultadoOperacion> AsignarPermisoARolAsync(int idRol, int idPermiso)
        {
            try
            {
                // Validar si ya existe la relación
                var existe = await _context.RolPermiso.AnyAsync(rp => rp.IdRol == idRol && rp.IdPermiso == idPermiso);

                if (existe)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Error = "El permiso ya está asignado a este rol."
                    };
                }

                var rolPermiso = new RolPermiso
                {
                    IdRol = idRol,
                    IdPermiso = idPermiso
                };

                _context.RolPermiso.Add(rolPermiso);
                await _context.SaveChangesAsync();

                return new ResultadoOperacion { Exito = true };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion { Exito = false, Error = ex.Message };
            }
        }

        // Actualizar el permiso de un rol
        public async Task<ResultadoOperacion> ActualizarPermisoDeRolAsync(int idRol, int idPermisoActual, int idPermisoNuevo)
        {
            try
            {
                // Buscar la relación existente
                var rolPermiso = await _context.RolPermiso.FirstOrDefaultAsync(rp => rp.IdRol == idRol && rp.IdPermiso == idPermisoActual);

                if (rolPermiso == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Error = "La relación Rol-Permiso no existe."
                    };
                }
                // Validar que el nuevo permiso exista en la tabla de permisos 
                var permisoExiste = await _context.Permisos.AnyAsync(p => p.IdPermiso == idPermisoNuevo);
                if (!permisoExiste)
                {
                    return new ResultadoOperacion { Exito = false, Error = "El permiso nuevo no existe." };
                }
                // Validar que el nuevo permiso no esté ya asignado al rol
                var existe = await _context.RolPermiso.AnyAsync(rp => rp.IdRol == idRol && rp.IdPermiso == idPermisoNuevo);

                if (existe)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Error = "El rol ya tiene asignado ese nuevo permiso."
                    };
                }

                // Actualizar el permiso
                rolPermiso.IdPermiso = idPermisoNuevo;
                await _context.SaveChangesAsync();

                return new ResultadoOperacion { Exito = true };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Error = $"Error inesperado: {ex.Message}"
                };
            }
        }

        // Eliminar un permiso de un rol
        public async Task<ResultadoOperacion> EliminarPermisoDeRolAsync(int idRol, int idPermiso)
        {
            try
            {
                var rolPermiso = await _context.RolPermiso.FirstOrDefaultAsync(rp => rp.IdRol == idRol && rp.IdPermiso == idPermiso);

                if (rolPermiso == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Error = "La relación Rol-Permiso no existe."
                    };
                }

                _context.RolPermiso.Remove(rolPermiso);
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
