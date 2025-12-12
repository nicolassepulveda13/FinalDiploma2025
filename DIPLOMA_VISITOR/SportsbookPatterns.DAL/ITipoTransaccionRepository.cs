using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL
{
    public interface ITipoTransaccionRepository
    {
        List<TipoTransaccion> GetAll();
        TipoTransaccion? GetById(int tipoTransaccionId);
        TipoTransaccion? GetByCodigo(string codigoTipo);
    }
}

