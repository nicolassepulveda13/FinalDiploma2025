using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class EstadoBloqueada : IEstadoCuenta
    {
        public void Apostar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            throw new Exception("No se puede apostar. La cuenta está bloqueada.");
        }

        public void Retirar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            throw new Exception("No se puede retirar. La cuenta está bloqueada.");
        }

        public void Depositar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            throw new Exception("No se puede depositar. La cuenta está bloqueada.");
        }

        public string GetNombreEstado()
        {
            return "Bloqueada";
        }
    }
}

