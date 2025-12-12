using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Services
{
    public class CuentaUsuarioService
    {
        private readonly ICuentaUsuarioRepository _cuentaRepo;
        private readonly ITransaccionRepository _transaccionRepo;

        public CuentaUsuarioService(
            ICuentaUsuarioRepository cuentaRepo,
            ITransaccionRepository transaccionRepo)
        {
            _cuentaRepo = cuentaRepo;
            _transaccionRepo = transaccionRepo;
        }

        public CuentaUsuario? GetCuentaByUsuarioId(int usuarioId)
        {
            return _cuentaRepo.GetByUsuarioId(usuarioId);
        }

        public List<Transaccion> GetTransaccionesByCuenta(int cuentaId)
        {
            return _transaccionRepo.GetByCuentaId(cuentaId);
        }
    }
}

