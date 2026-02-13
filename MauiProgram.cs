using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaLogin.Models.Data;
using PruebaLogin.ViewModels.Login;
using PruebaLogin.ViewModels.PermissionController;
using PruebaLogin.ViewModels.PUsuariosController;
using PruebaLogin.ViewModels.RolController;
using PruebaLogin.ViewModels.RPController;
using PruebaLogin.ViewModels.UserController;

namespace PruebaLogin
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-7HKQF3L;Database=Blazor;User Id=sa;Password=Adm1nTr4baj0;TrustServerCertificate=True;");
            });
            builder.Services.AddScoped<UsuarioController>();
            builder.Services.AddScoped<RolService>();
            builder.Services.AddScoped<PermisoService>();
            builder.Services.AddScoped<RolPermisoService>();
            
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddSingleton<AuthService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
