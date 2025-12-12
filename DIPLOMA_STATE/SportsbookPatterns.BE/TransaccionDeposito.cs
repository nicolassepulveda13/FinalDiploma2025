namespace SportsbookPatterns.BE
{
    public class TransaccionDeposito
    {
        public int TransaccionDepositoId { get; set; }
        public int TransaccionId { get; set; }
        public string MetodoDeposito { get; set; }
        public string? NumeroCuentaOrigen { get; set; }
        public string? BancoOrigen { get; set; }
        public string? NumeroReferencia { get; set; }
        public decimal Bonificacion { get; set; }
        
        public Transaccion? Transaccion { get; set; }
    }
}

