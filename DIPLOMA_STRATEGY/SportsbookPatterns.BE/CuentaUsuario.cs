namespace SportsbookPatterns.BE
{
    public class CuentaUsuario
    {
        public int CuentaId { get; set; }
        public int UsuarioId { get; set; }
        public decimal Saldo { get; set; }
        public int EstadoCuentaId { get; set; }
        public DateTime FechaCreacion { get; set; }


        public DateTime? FechaUltimaModificacion { get; set; }
    }
}

