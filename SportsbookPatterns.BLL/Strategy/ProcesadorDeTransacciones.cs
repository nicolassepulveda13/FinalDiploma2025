using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.DTOs;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.BLL.Strategy
{
    public class ProcesadorDeTransacciones
    {
        private IOperacion? _estrategia;
        private readonly ICuentaUsuarioRepository _cuentaRepo;
        private readonly ITransaccionRepository _transaccionRepo;

        public ProcesadorDeTransacciones(
            ICuentaUsuarioRepository cuentaRepo,
            ITransaccionRepository transaccionRepo)
        {
            _cuentaRepo = cuentaRepo;
            _transaccionRepo = transaccionRepo;
        }

        public void SetStrategy(IOperacion estrategia)
        {
            _estrategia = estrategia;
        }

        public ResultadoTransaccion ProcessTransaction(Transaccion transaccion)
        {
            if (_estrategia == null)
                throw new Exception("No se ha establecido una estrategia.");

            return _estrategia.ExecuteTransaction(transaccion, _cuentaRepo, _transaccionRepo);
        }
    }
}
