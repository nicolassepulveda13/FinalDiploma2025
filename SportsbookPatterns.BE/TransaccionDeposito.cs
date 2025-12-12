using SportsbookPatterns.BE.Visitor;

namespace SportsbookPatterns.BE
{
    public class TransaccionDeposito : ITransaccionVisitable
    {
        public int TransaccionDepositoId { get; set; }
        public int TransaccionId { get; set; }
        public string MetodoDeposito { get; set; }
        public string? NumeroCuentaOrigen { get; set; }
        public string? BancoOrigen { get; set; }
        public string? NumeroReferencia { get; set; }
        public decimal Bonificacion { get; set; }
        
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

