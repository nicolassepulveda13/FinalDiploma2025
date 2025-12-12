using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.BLL.Strategy;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Services
{
    public class ApuestaService
    {
        private readonly ProcesadorDeTransacciones _context;
        private readonly ITipoTransaccionRepository _tipoRepo;

        public ApuestaService(
            ProcesadorDeTransacciones procesador,
            ITipoTransaccionRepository tipoRepo)
        {
            _context = procesador;
            _tipoRepo = tipoRepo;
        }

        public ResultadoTransaccion ProcesarApuesta(CuentaUsuario cuenta, decimal monto, string tipoOperacion)
        {
            IOperacion estrategia = EstrategiaFactory.CrearEstrategia(tipoOperacion);
            
            string codigoTipo = estrategia.GetNombreOperacion() switch
            {
                "Crédito" => "Credito",
                "Débito" => "Debito",
                "Cancelación" => "Cancelacion",
                _ => "Cancelacion"
            };

            var tipoTransaccion = _tipoRepo.GetByCodigo(codigoTipo);
            if (tipoTransaccion == null)
                throw new Exception("Tipo de transacción no encontrado.");

            var transaccion = new Transaccion
            {
                CuentaId = cuenta.CuentaId,
                TipoTransaccionId = tipoTransaccion.TipoTransaccionId,
                Monto = monto,
                FechaTransaccion = DateTime.Now,
                Descripcion = $"Apuesta - {estrategia.GetNombreOperacion()}",
                Exitoso = estrategia.GetNombreOperacion() != "Cancelación"
            };

            _context.SetStrategy(estrategia);
            
            string eventoDeportivo = "Apuesta desde Strategy";
            string equipoApostado = estrategia.GetNombreOperacion() switch
            {
                "Crédito" => "Ganador",
                "Débito" => "Perdedor",
                _ => "Pendiente"
            };
            decimal cuota = estrategia.GetNombreOperacion() switch
            {
                "Crédito" => 2.5m,
                "Débito" => 1.5m,
                _ => 2.0m
            };

            return _context.ProcessTransaction(transaccion, esApuesta: true, eventoDeportivo, equipoApostado, cuota);
        }
    }
}

