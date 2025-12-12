using SportsbookPatterns.BE;
using SportsbookPatterns.BE.Visitor;
using SportsbookPatterns.BLL.Visitor;

namespace SportsbookPatterns.BLL.Services
{
    public class ReporteService
    {
        private readonly TransaccionService _transaccionService;

        public ReporteService(TransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        public object GenerarReporteImpuestos()
        {
            var visitor = new CalculadoraImpuestosVisitor();
            var elementos = _transaccionService.GetTransaccionesVisitable();

            foreach (var elemento in elementos)
            {
                elemento.Accept(visitor);
            }

            return visitor.GetResultado();
        }

        public object GenerarReporteComisiones()
        {
            var visitor = new GeneradorComisionesVisitor();
            var elementos = _transaccionService.GetTransaccionesVisitable();

            foreach (var elemento in elementos)
            {
                elemento.Accept(visitor);
            }

            return visitor.GetResultado();
        }

        public (object Impuestos, object Comisiones) GenerarReporteGeneral()
        {
            var visitorImpuestos = new CalculadoraImpuestosVisitor();
            var visitorComisiones = new GeneradorComisionesVisitor();
            var elementos = _transaccionService.GetTransaccionesVisitable();

            foreach (var elemento in elementos)
            {
                elemento.Accept(visitorImpuestos);
                elemento.Accept(visitorComisiones);
            }

            return (visitorImpuestos.GetResultado(), visitorComisiones.GetResultado());
        }
    }
}

