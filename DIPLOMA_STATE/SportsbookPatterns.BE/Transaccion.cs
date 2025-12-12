namespace SportsbookPatterns.BE
{
    public class Transaccion
    {
        public int TransaccionId { get; set; }
        public int CuentaId { get; set; }
        public int TipoTransaccionId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string? Descripcion { get; set; }
        public bool Exitoso { get; set; }
        public string? Observaciones { get; set; }
    }
}

