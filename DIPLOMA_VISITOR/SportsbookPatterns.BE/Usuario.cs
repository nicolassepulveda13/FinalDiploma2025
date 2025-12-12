namespace SportsbookPatterns.BE
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}

