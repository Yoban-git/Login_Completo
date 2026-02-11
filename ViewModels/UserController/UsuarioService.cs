using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models.Data;
using PruebaLogin.Models.User;

namespace PruebaLogin.ViewModels.UserController
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public class ResultadoOperacion { public bool Exito { get; set; } public string Error { get; set; } }
        public async Task<ResultadoOperacion> GuardarUsuarioAsync(Usuarioguid usuario)
        {
            try
            {
                usuario.Id = Guid.NewGuid();
                _context.Usuarioguid.Add(usuario);
                
                await _context.SaveChangesAsync();
                return new ResultadoOperacion { Exito = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResultadoOperacion { Exito = false, Error = ex.Message };
            }
        }
        public async Task<bool> ActualizarUsuarioAsync(Usuario usuario)
        {
            try
            {
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Usuarioguid>> ObtenerUsuariosAsync()
        {
            try
            {
                return await _context.Usuarioguid.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return new List<Usuarioguid>();
            }
        }
    }
}
