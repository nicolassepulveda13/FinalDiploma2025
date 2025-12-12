namespace SportsbookPatterns.BE
{
    public class LogTransaccion
    {
        public int LogId { get; set; }
        public int? TransaccionId { get; set; }
        public string TipoOperacion { get; set; }
        public string Mensaje { get; set; }
        public bool Exitoso { get; set; }
        public DateTime FechaLog { get; set; }
        public string? DetallesAdicionales { get; set; }
    }
}

