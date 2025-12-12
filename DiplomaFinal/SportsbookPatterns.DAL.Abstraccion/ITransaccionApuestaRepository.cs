using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL.Abstraccion
{
    public interface ITransaccionApuestaRepository
    {
        TransaccionApuesta? GetByTransaccionId(int transaccionId);
        List<TransaccionApuesta> GetAll();
        int Insert(TransaccionApuesta apuesta);
        bool Update(TransaccionApuesta apuesta);
    }
}

