using SportsbookPatterns.BE;

namespace SportsbookPatterns.BE.Visitor
{
    public interface ITransaccionVisitable
    {
        void Accept(ITransaccionVisitor visitor);
        int TransaccionIdProp { get; }
        decimal Monto { get; }
        DateTime FechaTransaccion { get; }
    }
}

