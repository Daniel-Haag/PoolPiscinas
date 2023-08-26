using PoolPiscinas.Models;

namespace PoolPiscinas.Sessions
{
    public interface IUsuarioService
    {
        public Usuario Login(Usuario usuario);
    }
}
