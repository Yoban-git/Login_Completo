using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaLogin.Models.Data;
using PruebaLogin.ViewModels.Login;
using PruebaLogin.ViewModels.PUsuariosController;
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
                options.UseSqlServer("Server=DESKTOP-B62ECE6;Database=Blazor;User Id=sa;Password=Adm1nTr4baj0;TrustServerCertificate=True;");
            });
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<UsuarioController>();
            builder.Services.AddSingleton<AuthService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
