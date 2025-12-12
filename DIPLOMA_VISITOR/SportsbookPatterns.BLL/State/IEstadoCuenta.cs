using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public interface IEstadoCuenta
    {
        void Apostar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo, ITransaccionApuestaRepository apuestaRepo, ITipoTransaccionRepository tipoRepo);
        void Retirar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo, ITransaccionRetiroRepository retiroRepo, ITipoTransaccionRepository tipoRepo);
        void Depositar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo, ITransaccionDepositoRepository depositoRepo, ITipoTransaccionRepository tipoRepo);
        string GetNombreEstado();
    }
}

