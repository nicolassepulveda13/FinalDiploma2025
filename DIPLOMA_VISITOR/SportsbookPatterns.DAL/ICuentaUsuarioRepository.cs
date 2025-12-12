using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL
{
    public interface ICuentaUsuarioRepository
    {
        CuentaUsuario? GetByUsuarioId(int usuarioId);
        CuentaUsuario? GetById(int cuentaId);
        decimal GetSaldo(int cuentaId);
        bool UpdateSaldo(int cuentaId, decimal nuevoSaldo);
        bool UpdateEstado(int cuentaId, int estadoCuentaId);
        int Insert(CuentaUsuario cuenta);
        bool Update(CuentaUsuario cuenta);
    }
}

