using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL.Abstraccion
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAll();
        Usuario? GetById(int usuarioId);
        int Insert(Usuario usuario);
        bool Update(Usuario usuario);
        bool Delete(int usuarioId);
    }
}

