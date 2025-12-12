using SportsbookPatterns.BE.Visitor;

namespace SportsbookPatterns.BE
{
    public class TransaccionRetiro : ITransaccionVisitable
    {
        public int TransaccionRetiroId { get; set; }
        public int TransaccionId { get; set; }
        public string MetodoRetiro { get; set; }
        public string? NumeroCuentaDestino { get; set; }
        public string? BancoDestino { get; set; }
        public decimal Comision { get; set; }
        public DateTime? FechaProcesamiento { get; set; }
        
        public Transaccion? Transaccion { get; set; }
        
        public void Accept(ITransaccionVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public int TransaccionIdProp => TransaccionId;
        public decimal Monto => Transaccion?.Monto ?? 0;
        public DateTime FechaTransaccion => Transaccion?.FechaTransaccion ?? DateTime.MinValue;
    }
}

