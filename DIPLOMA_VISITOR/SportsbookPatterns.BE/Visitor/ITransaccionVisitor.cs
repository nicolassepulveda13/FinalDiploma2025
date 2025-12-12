using SportsbookPatterns.BE;

namespace SportsbookPatterns.BE.Visitor
{
    public interface ITransaccionVisitor
    {
        void Visit(TransaccionApuesta apuesta);
        void Visit(TransaccionRetiro retiro);
        void Visit(TransaccionDeposito deposito);
        object GetResultado();
    }
}

