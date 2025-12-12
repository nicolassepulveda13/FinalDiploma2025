using Microsoft.Extensions.DependencyInjection;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.DAL;
using DiplomaFinal.Forms;

namespace DiplomaFinal
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
            services.AddSingleton<ITransaccionRepository>(sp => new TransaccionRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionApuestaRepository>(sp => new TransaccionApuestaRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionRetiroRepository>(sp => new TransaccionRetiroRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionDepositoRepository>(sp => new TransaccionDepositoRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITipoTransaccionRepository>(sp => new TipoTransaccionRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ICuentaUsuarioRepository>(sp => new CuentaUsuarioRepository(sp.GetRequiredService<Acceso>()));

            services.AddTransient<TransaccionService>();
            services.AddTransient<ReporteService>();

            services.AddTransient<FrmVisitor>();

            ServiceProvider = services.BuildServiceProvider();

            var mainForm = ServiceProvider.GetRequiredService<FrmVisitor>();
            Application.Run(mainForm);
        }
    }
}
