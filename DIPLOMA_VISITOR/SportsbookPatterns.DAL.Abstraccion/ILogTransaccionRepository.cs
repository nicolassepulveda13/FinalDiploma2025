using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL.Abstraccion
{
    public interface ILogTransaccionRepository
    {
        void Insert(LogTransaccion log);
        List<LogTransaccion> GetByTransaccionId(int transaccionId);
        List<LogTransaccion> GetAll();
    }
}

