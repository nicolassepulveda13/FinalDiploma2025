using System.Text;
using SportsbookPatterns.BE;
using SportsbookPatterns.BE.Visitor;

namespace SportsbookPatterns.BLL.Visitor
{
    public class GeneradorComisionesVisitor : ITransaccionVisitor
    {
        private decimal _totalComisiones = 0;
        private List<string> _detalles = new List<string>();

        public void Visit(TransaccionApuesta apuesta)
        {
            decimal monto = apuesta.Transaccion?.Monto ?? apuesta.MontoApostado;
            decimal comision = monto * 0.03m;
            _totalComisiones += comision;
            _detalles.Add($"Apuesta: ${monto:F2} - Comisión 3%: ${comision:F2}");
        }

        public void Visit(TransaccionRetiro retiro)
        {
            decimal comision = retiro.Monto * 0.05m;
            _totalComisiones += comision;
            _detalles.Add($"Retiro: ${retiro.Monto:F2} - Comisión 5%: ${comision:F2}");
        }

        public void Visit(TransaccionDeposito deposito)
        {
            _detalles.Add($"Depósito: ${deposito.Monto:F2} - Sin comisiones");
        }

        public object GetResultado()
        {
            var resultado = new StringBuilder();
            resultado.AppendLine($"Total de Comisiones: ${_totalComisiones:F2}");
            resultado.AppendLine();
            resultado.AppendLine("Detalles:");
            if (_detalles.Count == 0)
            {
                resultado.AppendLine("No hay transacciones para calcular comisiones.");
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

