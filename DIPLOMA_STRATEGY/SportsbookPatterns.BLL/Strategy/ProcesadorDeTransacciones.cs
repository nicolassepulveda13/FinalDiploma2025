using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.DAL;

namespace SportsbookPatterns.BLL.Strategy
{
    public class ProcesadorDeTransacciones
    {
        private IOperacion? _estrategia;
        private readonly ICuentaUsuarioRepository _cuentaRepo;
        private readonly ITransaccionRepository _transaccionRepo;
        private readonly ITransaccionApuestaRepository _apuestaRepo;
        private readonly ITipoTransaccionRepository _tipoRepo;

        public ProcesadorDeTransacciones(
            ICuentaUsuarioRepository cuentaRepo,
            ITransaccionRepository transaccionRepo,
            ITransaccionApuestaRepository apuestaRepo,
            ITipoTransaccionRepository tipoRepo)
        {
            _cuentaRepo = cuentaRepo;
            _transaccionRepo = transaccionRepo;
            _apuestaRepo = apuestaRepo;
            _tipoRepo = tipoRepo;
        }

        public void SetStrategy(IOperacion estrategia)
        {
            _estrategia = estrategia;
        }

        public ResultadoTransaccion ProcessTransaction(Transaccion transaccion, bool esApuesta = false, string? eventoDeportivo = null, string? equipoApostado = null, decimal? cuota = null)
        {
            if (_estrategia == null)
                throw new Exception("No se ha establecido una estrategia.");

            var resultado = _estrategia.ExecuteTransaction(transaccion, _cuentaRepo, _transaccionRepo);

            if (resultado.Exitoso && resultado.TransaccionId > 0 && esApuesta)
            {
                var apuesta = new TransaccionApuesta
                {
                    TransaccionId = resultado.TransaccionId,
                    EventoDeportivo = eventoDeportivo ?? "Apuesta desde Strategy",
                    EquipoApostado = equipoApostado ?? "Equipo Seleccionado",
                    Cuota = cuota ?? 2.0m,
                    MontoApostado = transaccion.Monto,
                    Resultado = (transaccion.Descripcion?.Contains("Gana") == true) ? "Ganó" : (transaccion.Descripcion?.Contains("Pierde") == true) ? "Perdió" : "Pendiente",
                    FechaEvento = DateTime.Now.AddDays(1)
                };
                _apuestaRepo.Insert(apuesta);
            }

            return resultado;
        }
    }
}

