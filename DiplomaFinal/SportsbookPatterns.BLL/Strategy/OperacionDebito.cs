using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Strategy
{
    public class OperacionDebito : IOperacion
    {
        public ResultadoTransaccion ExecuteTransaction(Transaccion transaccion, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            var cuenta = cuentaRepo.GetById(transaccion.CuentaId);
            if (cuenta == null)
                throw new Exception("Cuenta no encontrada.");

            decimal saldoAnterior = cuenta.Saldo;

            if (cuenta.Saldo < transaccion.Monto)
            {
                transaccion.Exitoso = false;
                transaccion.Observaciones = "Saldo insuficiente";
                int transaccionId = transaccionRepo.Insert(transaccion);
                
                return new ResultadoTransaccion
                {
                    Exitoso = false,
                    Mensaje = "No hay fondos suficientes para realizar el débito.",
                    SaldoAnterior = saldoAnterior,
                    SaldoNuevo = saldoAnterior,
                    TransaccionId = transaccionId
                };
            }

            decimal nuevoSaldo = saldoAnterior - transaccion.Monto;
            cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            transaccion.Exitoso = true;
            transaccion.Observaciones = "Débito realizado correctamente";
            int id = transaccionRepo.Insert(transaccion);

            return new ResultadoTransaccion
            {
                Exitoso = true,
                Mensaje = "Débito realizado correctamente.",
                SaldoAnterior = saldoAnterior,
                SaldoNuevo = nuevoSaldo,
                TransaccionId = id
            };
        }

        public string GetNombreOperacion()
        {
            return "Débito";
        }
    }
}

