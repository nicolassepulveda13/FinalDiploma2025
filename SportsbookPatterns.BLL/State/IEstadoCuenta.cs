using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public interface IEstadoCuenta
    {
        void Apostar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
        void Retirar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
        void Depositar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
        string GetNombreEstado();
    }
}

