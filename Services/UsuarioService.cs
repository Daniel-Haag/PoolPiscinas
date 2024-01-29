using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolPiscinas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public UsuarioService(PoolPiscinasDbContext poolPiscinasDbContext) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public Usuario GetUsuarioByCPForCNPJandSenha(string CPF, string CNPJ, string senha)
        {
            if (!string.IsNullOrEmpty(CPF))
            {
                var usuario = _poolPiscinasDbContext.Usuarios
                                .FirstOrDefault(x => x.CPF == CPF && senha == x.Senha);

                return usuario;
            }
            else if (!string.IsNullOrEmpty(CNPJ))
            {
                var usuario = _poolPiscinasDbContext.Usuarios
                                .FirstOrDefault(x => x.CNPJ == CNPJ && senha == x.Senha);

                return usuario;
            }
            else
            {
                return null;
            }
        }

        public void CreateNewUser(Usuario usuario)
        {
            try
            {
                _poolPiscinasDbContext.Usuarios.Add(usuario);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro de usuário");
            }
        }
    }
}
