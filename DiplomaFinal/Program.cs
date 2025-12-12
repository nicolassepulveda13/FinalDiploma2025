using Microsoft.Extensions.DependencyInjection;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.BLL.State;
using SportsbookPatterns.BLL.Strategy;
using SportsbookPatterns.BLL.Visitor;
using SportsbookPatterns.DAL.Abstraccion;
using SportsbookPatterns.DAL.Implementacion;
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
            services.AddSingleton<IUsuarioRepository>(sp => new UsuarioRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ICuentaUsuarioRepository>(sp => new CuentaUsuarioRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionRepository>(sp => new TransaccionRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionApuestaRepository>(sp => new TransaccionApuestaRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionRetiroRepository>(sp => new TransaccionRetiroRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITransaccionDepositoRepository>(sp => new TransaccionDepositoRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<IEstadoCuentaRepository>(sp => new EstadoCuentaRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ITipoTransaccionRepository>(sp => new TipoTransaccionRepository(sp.GetRequiredService<Acceso>()));
            services.AddSingleton<ILogTransaccionRepository>(sp => new LogTransaccionRepository(sp.GetRequiredService<Acceso>()));

            services.AddTransient<UsuarioService>();
            services.AddTransient<TransaccionService>();
            services.AddTransient<ReporteService>();
            services.AddTransient<ApuestaService>();
            services.AddTransient<CuentaUsuarioService>();
            services.AddTransient<ProcesadorDeTransacciones>();

            services.AddTransient<IEstadoCuenta, EstadoCreada>();
            services.AddTransient<IEstadoCuenta, EstadoActivaSinFondos>();
            services.AddTransient<IEstadoCuenta, EstadoActivaConFondos>();
            services.AddTransient<IEstadoCuenta, EstadoBloqueada>();

            services.AddTransient<IOperacion, OperacionDebito>();
            services.AddTransient<IOperacion, OperacionCredito>();
            services.AddTransient<IOperacion, OperacionCancelacion>();

            services.AddTransient<CalculadoraImpuestosVisitor>();
            services.AddTransient<GeneradorComisionesVisitor>();

            services.AddTransient<FrmMenuPrincipal>();
            services.AddTransient<FrmState>();
            services.AddTransient<FrmStrategy>();
            services.AddTransient<FrmVisitor>();

            ServiceProvider = services.BuildServiceProvider();

            var mainForm = ServiceProvider.GetRequiredService<FrmMenuPrincipal>();
            Application.Run(mainForm);
        }
    }
}
