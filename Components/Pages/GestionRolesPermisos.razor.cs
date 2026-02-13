using Microsoft.AspNetCore.Components;
using PruebaLogin.Models.Permisos;
using PruebaLogin.Models.Roles;
using PruebaLogin.ViewModels.PermissionController;
using PruebaLogin.ViewModels.RolController;
using PruebaLogin.ViewModels.RPController;

namespace PruebaLogin.Components.Pages
{
    public partial class RolesPermisosList : ComponentBase
    {
        private List<Roles2>? roles;
        private List<Permisos2>? permisos;
        private Roles2? rolEditando;
        private Permisos2? permisoEditando;
        private Roles2 nuevoRol = new Roles2();
        private Permisos2 nuevoPermiso = new Permisos2();
        private bool guardando = false;
        private bool agregandoRol = false;
        private bool agregandoPermiso = false;

        [Inject] private RolService _rolController { get; set; }
        [Inject] private PermisoService _permisoController { get; set; }

        protected override async Task OnInitializedAsync()
        {
            roles = await _rolController.ObtenerTodosRolesAsync();
            permisos = await _permisoController.ObtenerTodosPermisosAsync();
        }
        
        private void MostrarFormularioEdicionRol(Roles2 rol)
        {
            rolEditando = new Roles2 { IdRol = rol.IdRol, NombreRol = rol.NombreRol };
        }

        private async Task AplicarCambiosRol()
        {
            guardando = true;
            var resultado = await _rolController.ActualizarRolAsync(rolEditando);
            guardando = false;

            if (resultado.Exito)
            {
                roles = await _rolController.ObtenerTodosRolesAsync();
                rolEditando = null;
            }
        }

        private void MostrarFormularioEdicionPermiso(Permisos2 permiso)
        {
            permisoEditando = new Permisos2 { IdPermiso = permiso.IdPermiso, NombrePermiso = permiso.NombrePermiso };
        }

        private async Task AplicarCambiosPermiso()
        {
            guardando = true;
            var resultado = await _permisoController.ActualizarPermisoAsync(permisoEditando);
            guardando = false;

            if (resultado.Exito)
            {
                permisos = await _permisoController.ObtenerTodosPermisosAsync();
                permisoEditando = null;
            }
        }

        private void AgregarRol()
        {
            nuevoRol = new Roles2();
            agregandoRol = true;
        }

        private async Task GuardarNuevoRol()
        {
            guardando = true;
            var resultado = await _rolController.AgregarRolAsync(nuevoRol);
            guardando = false;

            if (resultado.Exito)
            {
                roles = await _rolController.ObtenerTodosRolesAsync();
                agregandoRol = false;
            }
        }

        private void AgregarPermiso()
        {
            nuevoPermiso = new Permisos2();
            agregandoPermiso = true;
        }

        private async Task GuardarNuevoPermiso()
        {
            guardando = true;
            var resultado = await _permisoController.AgregarPermisoAsync(nuevoPermiso);
            guardando = false;

            if (resultado.Exito)
            {
                permisos = await _permisoController.ObtenerTodosPermisosAsync();
                agregandoPermiso = false;
            }
        }

        private void Cancelar()
        {
            rolEditando = null;
            permisoEditando = null;
            agregandoRol = false;
            agregandoPermiso = false;
        }
    }
}
