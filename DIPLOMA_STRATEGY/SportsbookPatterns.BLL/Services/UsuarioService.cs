using SportsbookPatterns.BE;
using SportsbookPatterns.DAL;

namespace SportsbookPatterns.BLL.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public UsuarioService(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public List<Usuario> GetAll()
        {
            return _usuarioRepo.GetAll();
        }

        public Usuario? GetById(int usuarioId)
        {
            return _usuarioRepo.GetById(usuarioId);
        }
    }
}

