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
            decimal comision = apuesta.Monto * 0.03m;
            _totalComisiones += comision;
            _detalles.Add($"Apuesta: ${apuesta.Monto:F2} - Comisión 3%: ${comision:F2}");
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
            return new
            {
                TotalComisiones = _totalComisiones,
                Detalles = _detalles
            };
        }
    }
}

