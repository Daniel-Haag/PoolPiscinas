using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;

namespace PoolPiscinas.Interfaces
{
    public interface IUsuarioService
    {
        public void CreateNewUser(Usuario usuario);
    }
}
