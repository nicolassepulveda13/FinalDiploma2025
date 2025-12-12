using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL.Abstraccion
{
    public interface ITransaccionDepositoRepository
    {
        TransaccionDeposito? GetByTransaccionId(int transaccionId);
        List<TransaccionDeposito> GetAll();
        int Insert(TransaccionDeposito deposito);
        bool Update(TransaccionDeposito deposito);
    }
}

