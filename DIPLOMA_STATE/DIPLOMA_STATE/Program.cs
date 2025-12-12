using Microsoft.Extensions.DependencyInjection;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.BLL.State;
using SportsbookPatterns.DAL.Abstraccion;
using SportsbookPatterns.DAL.Implementacion;
using DIPLOMA_STATE.Forms;

namespace DIPLOMA_STATE
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
            services.AddSingleton<ITransaccionRetiroRepository>(sp => new TransaccionRetiroRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionDepositoRepository>(sp => new TransaccionDepositoRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<IEstadoCuentaRepository>(sp => new EstadoCuentaRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITipoTransaccionRepository>(sp => new TipoTransaccionRepository(sp.GetRequiredService<Acceso>()));

            services.AddTransient<UsuarioService>();
            services.AddTransient<CuentaUsuarioService>();

            services.AddTransient<FrmState>();

            ServiceProvider = services.BuildServiceProvider();

            var mainForm = ServiceProvider.GetRequiredService<FrmState>();
            Application.Run(mainForm);
        }
    }
}

