using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.DAL;

namespace SportsbookPatterns.BLL.Strategy
{
    public class OperacionCancelacion : IOperacion
    {
        public ResultadoTransaccion ExecuteTransaction(Transaccion transaccion, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            var cuenta = cuentaRepo.GetById(transaccion.CuentaId);
            if (cuenta == null)
                throw new Exception("Cuenta no encontrada.");

            decimal saldoAnterior = cuenta.Saldo;
            
            transaccion.Exitoso = false;
            transaccion.Observaciones = "Transacción cancelada";
            int id = transaccionRepo.Insert(transaccion);

            return new ResultadoTransaccion
            {
                Exitoso = false,
                Mensaje = "Transacción cancelada. El saldo no fue modificado.",
                SaldoAnterior = saldoAnterior,
                SaldoNuevo = saldoAnterior,
                TransaccionId = id
            };
        }

        public string GetNombreOperacion()
        {
            return "Cancelación";
        }
    }
}

