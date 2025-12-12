using SportsbookPatterns.BE;
using SportsbookPatterns.BE.Visitor;
using SportsbookPatterns.DAL;

namespace SportsbookPatterns.BLL.Services
{
    public class TransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepo;
        private readonly ITransaccionApuestaRepository _apuestaRepo;
        private readonly ITransaccionRetiroRepository _retiroRepo;
        private readonly ITransaccionDepositoRepository _depositoRepo;
        private readonly ITipoTransaccionRepository _tipoRepo;
        private readonly ICuentaUsuarioRepository _cuentaRepo;

        public TransaccionService(
            ITransaccionRepository transaccionRepo,
            ITransaccionApuestaRepository apuestaRepo,
            ITransaccionRetiroRepository retiroRepo,
            ITransaccionDepositoRepository depositoRepo,
            ITipoTransaccionRepository tipoRepo,
            ICuentaUsuarioRepository cuentaRepo)
        {
            _transaccionRepo = transaccionRepo;
            _apuestaRepo = apuestaRepo;
            _retiroRepo = retiroRepo;
            _depositoRepo = depositoRepo;
            _tipoRepo = tipoRepo;
            _cuentaRepo = cuentaRepo;
        }

        public List<Transaccion> GetTransaccionesByCuenta(int cuentaId)
        {
            return _transaccionRepo.GetByCuentaId(cuentaId);
        }

        public List<ITransaccionVisitable> GetTransaccionesVisitable()
        {
            var transacciones = _transaccionRepo.GetAll();
            var elementos = new List<ITransaccionVisitable>();

            foreach (var transaccion in transacciones)
            {
                var apuesta = _apuestaRepo.GetByTransaccionId(transaccion.TransaccionId);
                if (apuesta != null)
                {
                    apuesta.Transaccion = transaccion;
                    elementos.Add(apuesta);
                    continue;
                }

                var retiro = _retiroRepo.GetByTransaccionId(transaccion.TransaccionId);
                if (retiro != null)
                {
                    retiro.Transaccion = transaccion;
                    elementos.Add(retiro);
                    continue;
                }

                var deposito = _depositoRepo.GetByTransaccionId(transaccion.TransaccionId);
                if (deposito != null)
                {
                    deposito.Transaccion = transaccion;
                    elementos.Add(deposito);
                }
            }

            return elementos;
        }

        public int CrearApuesta(decimal monto, int cuentaId = 1)
        {
            var tipoDebito = _tipoRepo.GetByCodigo("Debito");
            if (tipoDebito == null)
                throw new Exception("Tipo de transacción 'Debito' no encontrado.");

            var transaccion = new Transaccion
            {
                CuentaId = cuentaId,
                TipoTransaccionId = tipoDebito.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = "Apuesta realizada",
                Exitoso = true
            };

            int transaccionId = _transaccionRepo.Insert(transaccion);

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

            _apuestaRepo.Insert(apuesta);

            return transaccionId;
        }

        public int CrearRetiro(decimal monto, int cuentaId = 1)
        {
            var tipoDebito = _tipoRepo.GetByCodigo("Debito");
            if (tipoDebito == null)
                throw new Exception("Tipo de transacción 'Debito' no encontrado.");

            var transaccion = new Transaccion
            {
                CuentaId = cuentaId,
                TipoTransaccionId = tipoDebito.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = "Retiro realizado",
                Exitoso = true
            };

            int transaccionId = _transaccionRepo.Insert(transaccion);

            var retiro = new TransaccionRetiro
            {
                TransaccionId = transaccionId,
                MetodoRetiro = "Transferencia Bancaria",
                NumeroCuentaDestino = "Cuenta Destino",
                BancoDestino = "Banco Destino",
                Comision = monto * 0.01m,
                FechaProcesamiento = DateTime.Now
            };

            _retiroRepo.Insert(retiro);

            return transaccionId;
        }

        public int CrearDeposito(decimal monto, int cuentaId = 1)
        {
            var tipoCredito = _tipoRepo.GetByCodigo("Credito");
            if (tipoCredito == null)
                throw new Exception("Tipo de transacción 'Credito' no encontrado.");

            var transaccion = new Transaccion
            {
                CuentaId = cuentaId,
                TipoTransaccionId = tipoCredito.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = "Depósito realizado",
                Exitoso = true
            };

            int transaccionId = _transaccionRepo.Insert(transaccion);

            var deposito = new TransaccionDeposito
            {
                TransaccionId = transaccionId,
                MetodoDeposito = "Transferencia Bancaria",
                NumeroCuentaOrigen = "Cuenta Origen",
                BancoOrigen = "Banco Origen",
                NumeroReferencia = $"REF{DateTime.Now:yyyyMMddHHmmss}",
                Bonificacion = 0
            };

            _depositoRepo.Insert(deposito);

            return transaccionId;
        }
    }
}

