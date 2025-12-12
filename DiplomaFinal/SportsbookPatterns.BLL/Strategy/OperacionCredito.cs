using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Strategy
{
    public class OperacionCredito : IOperacion
    {
        public ResultadoTransaccion ExecuteTransaction(Transaccion transaccion, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            var cuenta = cuentaRepo.GetById(transaccion.CuentaId);
            if (cuenta == null)
                throw new Exception("Cuenta no encontrada.");

            decimal saldoAnterior = cuenta.Saldo;
            decimal nuevoSaldo = saldoAnterior + transaccion.Monto;
            
            cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            transaccion.Exitoso = true;
            transaccion.Observaciones = "Crédito realizado correctamente";
            int id = transaccionRepo.Insert(transaccion);

            return new ResultadoTransaccion
            {
                Exitoso = true,
                Mensaje = "Crédito realizado correctamente.",
                SaldoAnterior = saldoAnterior,
                SaldoNuevo = nuevoSaldo,
                TransaccionId = id
            };
        }

        public string GetNombreOperacion()
        {
            return "Crédito";
        }
    }
}

