using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class EstadoActivaConFondos : IEstadoCuenta
    {
        public void Apostar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            if (monto <= 0)
                throw new Exception("El monto a apostar debe ser mayor a cero.");

            if (cuenta.Saldo < monto)
                throw new Exception("No hay fondos suficientes para realizar la apuesta.");

            decimal nuevoSaldo = cuenta.Saldo - monto;
            cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            cuenta.Saldo = nuevoSaldo;
        }

        public void Retirar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            if (monto <= 0)
                throw new Exception("El monto a retirar debe ser mayor a cero.");

            if (cuenta.Saldo < monto)
                throw new Exception("No hay fondos suficientes para realizar el retiro.");

            decimal nuevoSaldo = cuenta.Saldo - monto;
            cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            cuenta.Saldo = nuevoSaldo;
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
            return "ActivaConFondos";
        }
    }
}

