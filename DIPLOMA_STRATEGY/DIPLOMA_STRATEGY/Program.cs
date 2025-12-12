using Microsoft.Extensions.DependencyInjection;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.BLL.Strategy;
using SportsbookPatterns.DAL;
using DIPLOMA_STRATEGY.Forms;

namespace DIPLOMA_STRATEGY
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddSingleton<Acceso>();
            services.AddSingleton<IUsuarioRepository>(sp => new UsuarioRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ICuentaUsuarioRepository>(sp => new CuentaUsuarioRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionRepository>(sp => new TransaccionRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionApuestaRepository>(sp => new TransaccionApuestaRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITipoTransaccionRepository>(sp => new TipoTransaccionRepository(sp.GetRequiredService<Acceso>()));

            services.AddTransient<UsuarioService>();
            services.AddTransient<CuentaUsuarioService>();
            services.AddTransient<ApuestaService>();
            services.AddTransient<ProcesadorDeTransacciones>();

            services.AddTransient<FrmStrategy>();

            ServiceProvider = services.BuildServiceProvider();

            var mainForm = ServiceProvider.GetRequiredService<FrmStrategy>();
            Application.Run(mainForm);
        }
    }
}

