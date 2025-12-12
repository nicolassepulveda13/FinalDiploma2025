namespace SportsbookPatterns.BLL.State
{
    public interface IEstadoCuenta
    {
        void Apostar(CuentaContext context, decimal monto);
        void Retirar(CuentaContext context, decimal monto);
        void Depositar(CuentaContext context, decimal monto);
        string GetNombreEstado();
    }
}

