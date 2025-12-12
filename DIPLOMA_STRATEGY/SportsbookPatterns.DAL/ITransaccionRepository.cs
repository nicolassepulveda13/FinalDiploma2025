using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL
{
    public interface ITransaccionRepository
    {
        List<Transaccion> GetAll();
        List<Transaccion> GetByCuentaId(int cuentaId);
        Transaccion? GetById(int transaccionId);
        int Insert(Transaccion transaccion);
        bool Update(Transaccion transaccion);
        bool Delete(int transaccionId);
    }
}

