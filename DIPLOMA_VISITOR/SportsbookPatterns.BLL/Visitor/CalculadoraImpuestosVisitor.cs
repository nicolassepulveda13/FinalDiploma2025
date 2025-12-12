using System.Text;
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
            decimal monto = apuesta.Transaccion?.Monto ?? apuesta.MontoApostado;
            decimal impuesto = monto * 0.05m;
            _totalImpuestos += impuesto;
            _detalles.Add($"Apuesta: ${monto:F2} - Impuesto 5%: ${impuesto:F2}");
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
            var resultado = new StringBuilder();
            resultado.AppendLine($"Total de Impuestos: ${_totalImpuestos:F2}");
            resultado.AppendLine();
            resultado.AppendLine("Detalles:");
            if (_detalles.Count == 0)
            {
                resultado.AppendLine("No hay transacciones para calcular impuestos.");
            }
            else
            {
                foreach (var detalle in _detalles)
                {
                    resultado.AppendLine($"  - {detalle}");
                }
            }
            return resultado.ToString();
        }
    }
}

