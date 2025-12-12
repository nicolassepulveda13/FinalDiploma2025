using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL.Abstraccion
{
    public interface ITransaccionRetiroRepository
    {
        TransaccionRetiro? GetByTransaccionId(int transaccionId);
        List<TransaccionRetiro> GetAll();
        int Insert(TransaccionRetiro retiro);
        bool Update(TransaccionRetiro retiro);
    }
}

