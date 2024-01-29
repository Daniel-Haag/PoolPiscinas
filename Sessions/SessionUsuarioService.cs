using System.Collections.Generic;
using System.Linq;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;

namespace PoolPiscinas.Sessions
{
    public class SessionUsuarioService : ISessionUsuarioService
    {
        private readonly IUsuarioService _usuarioService;

        public SessionUsuarioService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario Login(Usuario usuarioLogin)
        {
            var usuario = _usuarioService
                .GetUsuarioByCPForCNPJandSenha(usuarioLogin.CPF, usuarioLogin.CNPJ, usuarioLogin.Senha);

            return usuario;
        }
    }
}
