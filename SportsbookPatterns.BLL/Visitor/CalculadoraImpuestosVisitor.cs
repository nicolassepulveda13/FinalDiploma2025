using SportsbookPatterns.BE;
using SportsbookPatterns.BE.Visitor;

namespace SportsbookPatterns.BLL.Visitor
{
    public class CalculadoraImpuestosVisitor : ITransaccionVisitor
    {
        private decimal _totalImpuestos = 0;
        private List<string> _detalles = new List<string>();

        public void Visit(TransaccionApuesta apuesta)
        {
            decimal impuesto = apuesta.Monto * 0.05m;
            _totalImpuestos += impuesto;
            _detalles.Add($"Apuesta: ${apuesta.Monto:F2} - Impuesto 5%: ${impuesto:F2}");
        }

        public void Visit(TransaccionRetiro retiro)
        {
            decimal impuesto = retiro.Monto * 0.02m;
            _totalImpuestos += impuesto;
            _detalles.Add($"Retiro: ${retiro.Monto:F2} - Impuesto 2%: ${impuesto:F2}");
        }

        public void Visit(TransaccionDeposito deposito)
        {
            _detalles.Add($"Depósito: ${deposito.Monto:F2} - Sin impuestos");
        }

        public object GetResultado()
        {
            return new
            {
                TotalImpuestos = _totalImpuestos,
                Detalles = _detalles
            };
        }
    }
}

