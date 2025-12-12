namespace SportsbookPatterns.BE
{
    public class TipoTransaccion
    {
        public int TipoTransaccionId { get; set; }
        public string CodigoTipo { get; set; }
        public string Descripcion { get; set; }
        public bool AfectaSaldo { get; set; }
        public bool Activo { get; set; }
    }
}

