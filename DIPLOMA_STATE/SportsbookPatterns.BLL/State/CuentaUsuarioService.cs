using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class CuentaUsuarioService
    {
        private readonly ICuentaUsuarioRepository _cuentaRepo;
        private readonly IEstadoCuentaRepository _estadoRepo;
        private readonly ITransaccionRepository _transaccionRepo;
        private readonly ITransaccionApuestaRepository _apuestaRepo;
        private readonly ITransaccionRetiroRepository _retiroRepo;
        private readonly ITransaccionDepositoRepository _depositoRepo;
        private readonly ITipoTransaccionRepository _tipoRepo;

        public CuentaUsuarioService(
            ICuentaUsuarioRepository cuentaRepo,
            IEstadoCuentaRepository estadoRepo,
            ITransaccionRepository transaccionRepo,
            ITransaccionApuestaRepository apuestaRepo,
            ITransaccionRetiroRepository retiroRepo,
            ITransaccionDepositoRepository depositoRepo,
            ITipoTransaccionRepository tipoRepo)
        {
            _cuentaRepo = cuentaRepo;
            _estadoRepo = estadoRepo;
            _transaccionRepo = transaccionRepo;
            _apuestaRepo = apuestaRepo;
            _retiroRepo = retiroRepo;
            _depositoRepo = depositoRepo;
            _tipoRepo = tipoRepo;
        }

        public CuentaContext CrearContext(CuentaUsuario cuenta)
        {
            return new CuentaContext(
                cuenta,
                _cuentaRepo,
                _estadoRepo,
                _transaccionRepo,
                _apuestaRepo,
                _retiroRepo,
                _depositoRepo,
                _tipoRepo);
        }

        public List<EstadoCuenta> GetAllEstados()
        {
            return _estadoRepo.GetAll();
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

