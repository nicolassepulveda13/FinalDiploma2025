using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class EstadoActiva : IEstadoCuenta
    {
        public void Apostar(CuentaContext context, decimal monto)
        {
            if (monto <= 0)
                throw new Exception("El monto a apostar debe ser mayor a cero.");

            var cuenta = context.Cuenta;
            if (cuenta.Saldo < monto)
                throw new Exception("No hay fondos suficientes para realizar la apuesta.");

            var tipoDebito = context._tipoRepo.GetByCodigo("Debito");
            if (tipoDebito == null)
                throw new Exception("Tipo de transacción 'Debito' no encontrado.");

            var transaccion = new Transaccion
            {
                CuentaId = cuenta.CuentaId,
                TipoTransaccionId = tipoDebito.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = "Apuesta realizada",
                Exitoso = true
            };

            int transaccionId = context._transaccionRepo.Insert(transaccion);

            var apuesta = new TransaccionApuesta
            {
                TransaccionId = transaccionId,
                EventoDeportivo = "Evento Deportivo",
                EquipoApostado = "Equipo Seleccionado",
                Cuota = 2.0m,
                MontoApostado = monto,
                Resultado = "Pendiente",
                FechaEvento = DateTime.Now.AddDays(1)
            };

            context._apuestaRepo.Insert(apuesta);

            decimal nuevoSaldo = cuenta.Saldo - monto;
            context._cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            cuenta.Saldo = nuevoSaldo;
        }

        public void Retirar(CuentaContext context, decimal monto)
        {
            if (monto <= 0)
                throw new Exception("El monto a retirar debe ser mayor a cero.");

            var cuenta = context.Cuenta;
            if (cuenta.Saldo < monto)
                throw new Exception("No hay fondos suficientes para realizar el retiro.");

            var tipoDebito = context._tipoRepo.GetByCodigo("Debito");
            if (tipoDebito == null)
                throw new Exception("Tipo de transacción 'Debito' no encontrado.");

            var transaccion = new Transaccion
            {
                CuentaId = cuenta.CuentaId,
                TipoTransaccionId = tipoDebito.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = "Retiro realizado",
                Exitoso = true
            };

            int transaccionId = context._transaccionRepo.Insert(transaccion);

            var retiro = new TransaccionRetiro
            {
                TransaccionId = transaccionId,
                MetodoRetiro = "Transferencia Bancaria",
                NumeroCuentaDestino = "Cuenta Destino",
                BancoDestino = "Banco Destino",
                Comision = monto * 0.01m,
                FechaProcesamiento = DateTime.Now
            };

            context._retiroRepo.Insert(retiro);

            decimal nuevoSaldo = cuenta.Saldo - monto;
            context._cuentaRepo.UpdateSaldo(cuenta.CuentaId, nuevoSaldo);
            cuenta.Saldo = nuevoSaldo;
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
        }

        public string GetNombreEstado()
        {
            return "Activa";
        }
    }
}

