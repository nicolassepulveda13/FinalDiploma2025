using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class CuentaContext
    {
        private IEstadoCuenta _estadoActual;
        private readonly CuentaUsuario _cuenta;
        public readonly ICuentaUsuarioRepository _cuentaRepo;
        private readonly IEstadoCuentaRepository _estadoRepo;
        public readonly ITransaccionRepository _transaccionRepo;
        public readonly ITransaccionApuestaRepository _apuestaRepo;
        public readonly ITransaccionRetiroRepository _retiroRepo;
        public readonly ITransaccionDepositoRepository _depositoRepo;
        public readonly ITipoTransaccionRepository _tipoRepo;

        public CuentaUsuario Cuenta => _cuenta;
        public IEstadoCuenta EstadoActual => _estadoActual;

        public CuentaContext(
            CuentaUsuario cuenta,
            ICuentaUsuarioRepository cuentaRepo,
            IEstadoCuentaRepository estadoRepo,
            ITransaccionRepository transaccionRepo,
            ITransaccionApuestaRepository apuestaRepo,
            ITransaccionRetiroRepository retiroRepo,
            ITransaccionDepositoRepository depositoRepo,
            ITipoTransaccionRepository tipoRepo)
        {
            _cuenta = cuenta;
            _cuentaRepo = cuentaRepo;
            _estadoRepo = estadoRepo;
            _transaccionRepo = transaccionRepo;
            _apuestaRepo = apuestaRepo;
            _retiroRepo = retiroRepo;
            _depositoRepo = depositoRepo;
            _tipoRepo = tipoRepo;

            var estadoBD = _estadoRepo.GetById(cuenta.EstadoCuentaId);
            if (estadoBD == null)
                throw new Exception("Estado de cuenta no encontrado.");

            _estadoActual = CrearEstado(estadoBD.CodigoEstado);
        }

        public void Apostar(decimal monto)
        {
            _estadoActual.Apostar(this, monto);
        }

        public void Retirar(decimal monto)
        {
            _estadoActual.Retirar(this, monto);
        }

        public void Depositar(decimal monto)
        {
            _estadoActual.Depositar(this, monto);
        }

        public void CambiarEstado(string codigoEstado)
        {
            var estadoBD = _estadoRepo.GetByCodigo(codigoEstado);
            if (estadoBD == null)
                throw new Exception($"Estado '{codigoEstado}' no encontrado.");

            _cuentaRepo.UpdateEstado(_cuenta.CuentaId, estadoBD.EstadoCuentaId);
            _cuenta.EstadoCuentaId = estadoBD.EstadoCuentaId;
            _estadoActual = CrearEstado(codigoEstado);
        }

        private IEstadoCuenta CrearEstado(string codigoEstado)
        {
            return codigoEstado switch
            {
                "Creada" => new EstadoCreada(),
                "Activa" => new EstadoActiva(),
                "Bloqueada" => new EstadoBloqueada(),
                _ => throw new Exception($"Estado '{codigoEstado}' no reconocido.")
            };
        }
    }
}

