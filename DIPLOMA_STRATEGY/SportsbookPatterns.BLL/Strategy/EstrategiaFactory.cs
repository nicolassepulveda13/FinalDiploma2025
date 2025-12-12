namespace SportsbookPatterns.BLL.Strategy
{
    public static class EstrategiaFactory
    {
        public static IOperacion CrearEstrategia(string tipoOperacion)
        {
            return tipoOperacion switch
            {
                "Gana" => new OperacionCredito(),
                "Pierde" => new OperacionDebito(),
                "Cancelar" => new OperacionCancelacion(),
                _ => new OperacionCancelacion()
            };
        }
    }
}

