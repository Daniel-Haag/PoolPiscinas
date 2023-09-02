using PoolPiscinas.Models;

namespace PoolPiscinas.Sessions
{
    public interface ISessionUsuarioService
    {
        public Usuario Login(Usuario usuario);
    }
}
