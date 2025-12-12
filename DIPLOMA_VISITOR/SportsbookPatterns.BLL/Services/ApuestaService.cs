using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.BLL.Strategy;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Services
{
    public class ApuestaService
    {
        private readonly ProcesadorDeTransacciones _procesador;
        private readonly ITipoTransaccionRepository _tipoRepo;

        public ApuestaService(
            ProcesadorDeTransacciones procesador,
            ITipoTransaccionRepository tipoRepo)
        {
            _procesador = procesador;
            _tipoRepo = tipoRepo;
        }

        public ResultadoTransaccion ProcesarApuesta(CuentaUsuario cuenta, decimal monto, string tipoOperacion)
        {
            var tipoTransaccion = _tipoRepo.GetByCodigo(tipoOperacion == "Gana" ? "Credito" : tipoOperacion == "Pierde" ? "Debito" : "Cancelacion");
            if (tipoTransaccion == null)
                throw new Exception("Tipo de transacción no encontrado.");

            IOperacion estrategia = tipoOperacion switch
            {
                "Gana" => new OperacionCredito(),
                "Pierde" => new OperacionDebito(),
                _ => new OperacionCancelacion()
            };

            var transaccion = new Transaccion
            {
                CuentaId = cuenta.CuentaId,
                TipoTransaccionId = tipoTransaccion.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = $"Apuesta - {tipoOperacion}",
                Exitoso = tipoOperacion != "Vuelve a intentar"
            };

            _procesador.SetStrategy(estrategia);
            
            string eventoDeportivo = "Apuesta desde Strategy";
            string equipoApostado = tipoOperacion == "Gana" ? "Ganador" : tipoOperacion == "Pierde" ? "Perdedor" : "Pendiente";
            decimal cuota = tipoOperacion == "Gana" ? 2.5m : tipoOperacion == "Pierde" ? 1.5m : 2.0m;

            return _procesador.ProcessTransaction(transaccion, esApuesta: true, eventoDeportivo, equipoApostado, cuota);
        }
    }
}

