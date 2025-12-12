using Microsoft.Extensions.DependencyInjection;
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

            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<ICuentaUsuarioRepository, CuentaUsuarioRepository>();
            services.AddSingleton<ITransaccionRepository, TransaccionRepository>();
            services.AddSingleton<ITransaccionApuestaRepository, TransaccionApuestaRepository>();
            services.AddSingleton<ITransaccionRetiroRepository, TransaccionRetiroRepository>();
            services.AddSingleton<ITransaccionDepositoRepository, TransaccionDepositoRepository>();
            services.AddSingleton<IEstadoCuentaRepository, EstadoCuentaRepository>();
            services.AddSingleton<ITipoTransaccionRepository, TipoTransaccionRepository>();
            services.AddSingleton<ILogTransaccionRepository, LogTransaccionRepository>();

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
