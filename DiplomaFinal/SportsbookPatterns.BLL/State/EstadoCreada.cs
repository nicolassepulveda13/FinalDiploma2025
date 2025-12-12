using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class EstadoCreada : IEstadoCuenta
    {
        public void Apostar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            throw new Exception("No se puede apostar. La cuenta está en estado Creada. Debe depositar primero.");
        }

        public void Retirar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            throw new Exception("No se puede retirar. La cuenta está en estado Creada. Debe depositar primero.");
        }

        public void Depositar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            if (monto <= 0)
                throw new Exception("El monto a depositar debe ser mayor a cero.");

            decimal nuevoSaldo = cuenta.Saldo + monto;
            cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            cuenta.Saldo = nuevoSaldo;
        }

        public string GetNombreEstado()
        {
            return "Creada";
        }
    }
}

