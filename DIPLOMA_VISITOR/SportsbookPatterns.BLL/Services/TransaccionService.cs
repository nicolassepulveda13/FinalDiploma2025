using SportsbookPatterns.BE;
using SportsbookPatterns.BE.Visitor;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Services
{
    public class TransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepo;
        private readonly ITransaccionApuestaRepository _apuestaRepo;
        private readonly ITransaccionRetiroRepository _retiroRepo;
        private readonly ITransaccionDepositoRepository _depositoRepo;

        public TransaccionService(
            ITransaccionRepository transaccionRepo,
            ITransaccionApuestaRepository apuestaRepo,
            ITransaccionRetiroRepository retiroRepo,
            ITransaccionDepositoRepository depositoRepo)
        {
            _transaccionRepo = transaccionRepo;
            _apuestaRepo = apuestaRepo;
            _retiroRepo = retiroRepo;
            _depositoRepo = depositoRepo;
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
    }
}

