namespace SportsbookPatterns.BLL.State
{
    public class EstadoBloqueada : IEstadoCuenta
    {
        public void Apostar(CuentaContext context, decimal monto)
        {
            throw new Exception("No se puede apostar. La cuenta está bloqueada.");
        }

        public void Retirar(CuentaContext context, decimal monto)
        {
            throw new Exception("No se puede retirar. La cuenta está bloqueada.");
        }

        public void Depositar(CuentaContext context, decimal monto)
        {
            throw new Exception("No se puede depositar. La cuenta está bloqueada.");
        }

        public string GetNombreEstado()
        {
            return "Bloqueada";
        }
    }
}

