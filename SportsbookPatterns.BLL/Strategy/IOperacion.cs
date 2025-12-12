using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Strategy
{
    public interface IOperacion
    {
        ResultadoTransaccion ExecuteTransaction(Transaccion transaccion, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
        string GetNombreOperacion();
    }
}

