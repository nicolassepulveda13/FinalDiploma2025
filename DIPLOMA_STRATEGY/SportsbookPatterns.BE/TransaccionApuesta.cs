namespace SportsbookPatterns.BE
{
    public class TransaccionApuesta
    {
        public int TransaccionApuestaId { get; set; }
        public int TransaccionId { get; set; }
        public string EventoDeportivo { get; set; }
        public string EquipoApostado { get; set; }
        public decimal Cuota { get; set; }
        public decimal MontoApostado { get; set; }
        public string? Resultado { get; set; }
        public DateTime? FechaEvento { get; set; }
        
        public Transaccion? Transaccion { get; set; }
    }
}

