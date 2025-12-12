using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class EstadoCreada : IEstadoCuenta
    {
        public void Apostar(CuentaContext context, decimal monto)
        {
            throw new Exception("No se puede apostar. La cuenta está en estado Creada. Debe depositar primero.");
        }

        public void Retirar(CuentaContext context, decimal monto)
        {
            throw new Exception("No se puede retirar. La cuenta está en estado Creada. Debe depositar primero.");
        }

        public void Depositar(CuentaContext context, decimal monto)
        {
            if (monto <= 0)
                throw new Exception("El monto a depositar debe ser mayor a cero.");

            var cuenta = context.Cuenta;
            var tipoCredito = context._tipoRepo.GetByCodigo("Credito");
            if (tipoCredito == null)
                throw new Exception("Tipo de transacción 'Credito' no encontrado.");

            var transaccion = new Transaccion
            {
                CuentaId = cuenta.CuentaId,
                TipoTransaccionId = tipoCredito.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = "Depósito realizado",
                Exitoso = true
            };

            int transaccionId = context._transaccionRepo.Insert(transaccion);

            var deposito = new TransaccionDeposito
            {
                TransaccionId = transaccionId,
                MetodoDeposito = "Transferencia Bancaria",
                NumeroCuentaOrigen = "Cuenta Origen",
                BancoOrigen = "Banco Origen",
                NumeroReferencia = $"REF{DateTime.Now:yyyyMMddHHmmss}",
                Bonificacion = 0
            };

            context._depositoRepo.Insert(deposito);

            decimal nuevoSaldo = cuenta.Saldo + monto;
            context._cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            cuenta.Saldo = nuevoSaldo;

            if (nuevoSaldo > 0)
            {
                context.CambiarEstado("Activa");
            }
        }

        public string GetNombreEstado()
        {
            return "Creada";
        }
    }
}

