namespace SportsbookPatterns.BLL.DTOs
{
    public class ReporteDTO
    {
        public string TipoReporte { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<string> Detalles { get; set; } = new List<string>();
        public DateTime FechaGeneracion { get; set; } = DateTime.Now;
    }
}

