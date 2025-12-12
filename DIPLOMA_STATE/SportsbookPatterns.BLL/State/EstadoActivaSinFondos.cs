using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class EstadoActivaSinFondos : IEstadoCuenta
    {
        public void Apostar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo, ITransaccionApuestaRepository apuestaRepo, ITipoTransaccionRepository tipoRepo)
        {
            throw new Exception("No se puede apostar. La cuenta no tiene fondos suficientes. Debe depositar primero.");
        }

        public void Retirar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo, ITransaccionRetiroRepository retiroRepo, ITipoTransaccionRepository tipoRepo)
        {
            throw new Exception("No se puede retirar. La cuenta no tiene fondos suficientes.");
        }

        public void Depositar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo, ITransaccionDepositoRepository depositoRepo, ITipoTransaccionRepository tipoRepo)
        {
            if (monto <= 0)
                throw new Exception("El monto a depositar debe ser mayor a cero.");

            decimal nuevoSaldo = cuenta.Saldo + monto;
            cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            cuenta.Saldo = nuevoSaldo;
        }

        public string GetNombreEstado()
        {
            return "ActivaSinFondos";
        }
    }
}

