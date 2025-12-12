namespace SportsbookPatterns.BE
{
    public class TransaccionRetiro
    {
        public int TransaccionRetiroId { get; set; }
        public int TransaccionId { get; set; }
        public string MetodoRetiro { get; set; }
        public string? NumeroCuentaDestino { get; set; }
        public string? BancoDestino { get; set; }
        public decimal Comision { get; set; }
        public DateTime? FechaProcesamiento { get; set; }
        
        public Transaccion? Transaccion { get; set; }
    }
}

