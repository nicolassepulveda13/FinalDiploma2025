namespace SportsbookPatterns.BLL.DTOs
{
    public class ResultadoTransaccion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }
        public int TransaccionId { get; set; }
    }
}

