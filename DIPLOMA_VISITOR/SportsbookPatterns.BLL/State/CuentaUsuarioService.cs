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

        public void CambiarEstado(CuentaUsuario cuenta, string codigoEstado)
        {
            var estado = _estadoRepo.GetByCodigo(codigoEstado);
            if (estado == null)
                throw new Exception($"Estado '{codigoEstado}' no encontrado.");

            _cuentaRepo.UpdateEstado(cuenta.CuentaId, estado.EstadoCuentaId);
            cuenta.EstadoCuentaId = estado.EstadoCuentaId;
        }

        public IEstadoCuenta ObtenerEstado(CuentaUsuario cuenta)
        {
            var estado = _estadoRepo.GetById(cuenta.EstadoCuentaId);
            if (estado == null)
                throw new Exception("Estado de cuenta no encontrado.");

            return CrearEstado(estado.CodigoEstado);
        }

        public void Apostar(CuentaUsuario cuenta, decimal monto)
        {
            var estado = ObtenerEstado(cuenta);
            estado.Apostar(cuenta, monto, _cuentaRepo, _transaccionRepo, _apuestaRepo, _tipoRepo);
        }

        public void Retirar(CuentaUsuario cuenta, decimal monto)
        {
            var estado = ObtenerEstado(cuenta);
            estado.Retirar(cuenta, monto, _cuentaRepo, _transaccionRepo, _retiroRepo, _tipoRepo);
        }

        public void Depositar(CuentaUsuario cuenta, decimal monto)
        {
            var estado = ObtenerEstado(cuenta);
            estado.Depositar(cuenta, monto, _cuentaRepo, _transaccionRepo, _depositoRepo, _tipoRepo);
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

        private IEstadoCuenta CrearEstado(string codigoEstado)
        {
            return codigoEstado switch
            {
                "Creada" => new EstadoCreada(),
                "ActivaSinFondos" => new EstadoActivaSinFondos(),
                "ActivaConFondos" => new EstadoActivaConFondos(),
                "Bloqueada" => new EstadoBloqueada(),
                _ => throw new Exception($"Estado '{codigoEstado}' no reconocido.")
            };
        }
    }
}

