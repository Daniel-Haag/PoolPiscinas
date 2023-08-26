using System.Collections.Generic;
using System.Linq;
using PoolPiscinas.Models;

namespace PoolPiscinas.Sessions
{
    public class UsuarioService : IUsuarioService
    {
        private List<Usuario> usuarios;

        public UsuarioService()
        {
            usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Nome = "Usuario 1",
                    Senha = "123"
                },
                new Usuario()
                {
                    Nome = "Usuario 2",
                    Senha = "123"
                }
            };

        }

        public Usuario Login(Usuario usuario)
        {
            return usuarios.SingleOrDefault(x => x.Nome == usuario.Nome && x.Senha == usuario.Senha);
        }
    }
}
