using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.Data;

namespace PruebaLogin.ViewModels.Login
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public event Action OnChange;
        public bool IsLoggedIn { get; private set; }
        public string CurrentUser { get; private set; }
        public Guid CurrentUserId { get; private set; }
        public string CurrentUserEmail { get; private set; }
        public string CurrentUserRol { get; private set; }//nuevo
        public AuthService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResultadoLogin> Login(string username, string password)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return new ResultadoLogin
                    {
                        Exito = false,
                        Mensaje = "Usuario y contraseña son requeridos"
                    };
                }

                // Buscar usuario por username o email (SIN filtrar por Activo)
                var user = await _context.Usuarios2
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);

                if (user == null)
                {
                    return new ResultadoLogin
                    {
                        Exito = false,
                        Mensaje = "Usuario o contraseña incorrectos"
                    };
                }

                // Verificar si el usuario está desactivado ANTES de verificar la contraseña
                if (!user.Activo)
                {
                    return new ResultadoLogin
                    {
                        Exito = false,
                        Mensaje = "Tu cuenta ha sido desactivada. Contacta al administrador."
                    };
                }

                // Verificar contraseña usando BCrypt
                bool passwordValida = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (!passwordValida)
                {
                    return new ResultadoLogin
                    {
                        Exito = false,
                        Mensaje = "Usuario o contraseña incorrectos"
                    };
                }

                // Login exitoso
                IsLoggedIn = true;
                CurrentUser = user.Nombre;
                CurrentUserId = user.Id;
                CurrentUserEmail = user.Email;
                CurrentUserRol = user.Rol; //nuevo
                NotifyStateChanged();

                return new ResultadoLogin
                {
                    Exito = true,
                    Mensaje = "Inicio de sesión exitoso"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Login: {ex.Message}");
                return new ResultadoLogin
                {
                    Exito = false,
                    Mensaje = "Error al iniciar sesión. Intenta nuevamente."
                };
            }
        }
        // NUEVO: Métodos helper para verificar roles
        public bool EsAdministrador() => CurrentUserRol == "Admin";
        public bool EsUsuario() => CurrentUserRol == "Usuario";
        //fin de lo nuevo
        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = null;
            CurrentUserId = Guid.Empty;
            CurrentUserEmail = null;
            CurrentUserRol = null; // NUEVO
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    // Clase para el resultado del login
    public class ResultadoLogin
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}
