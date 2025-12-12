using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.State
{
    public class CuentaUsuarioService
    {
        private readonly ICuentaUsuarioRepository _cuentaRepo;
        private readonly IEstadoCuentaRepository _estadoRepo;
        private readonly ITransaccionRepository _transaccionRepo;
        private IEstadoCuenta? _estadoActual;

        public CuentaUsuarioService(
            ICuentaUsuarioRepository cuentaRepo,
            IEstadoCuentaRepository estadoRepo,
            ITransaccionRepository transaccionRepo)
        {
            _cuentaRepo = cuentaRepo;
            _estadoRepo = estadoRepo;
            _transaccionRepo = transaccionRepo;
        }

        public void CambiarEstado(CuentaUsuario cuenta, string codigoEstado)
        {
            var estado = _estadoRepo.GetByCodigo(codigoEstado);
            if (estado == null)
                throw new Exception($"Estado '{codigoEstado}' no encontrado.");

            _cuentaRepo.UpdateEstado(cuenta.CuentaId, estado.EstadoCuentaId);
            cuenta.EstadoCuentaId = estado.EstadoCuentaId;

            _estadoActual = CrearEstado(codigoEstado);
        }

        public IEstadoCuenta ObtenerEstado(CuentaUsuario cuenta)
        {
            var estado = _estadoRepo.GetById(cuenta.EstadoCuentaId);
            if (estado == null)
                throw new Exception("Estado de cuenta no encontrado.");

            return CrearEstado(estado.CodigoEstado);
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

