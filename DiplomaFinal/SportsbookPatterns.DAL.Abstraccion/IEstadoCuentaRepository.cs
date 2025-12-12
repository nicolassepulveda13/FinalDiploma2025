using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL.Abstraccion
{
    public interface IEstadoCuentaRepository
    {
        List<EstadoCuenta> GetAll();
        EstadoCuenta? GetById(int estadoCuentaId);
        EstadoCuenta? GetByCodigo(string codigoEstado);
    }
}

