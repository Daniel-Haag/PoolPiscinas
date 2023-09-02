using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using System;

namespace PoolPiscinas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public UsuarioService(PoolPiscinasDbContext poolPiscinasDbContext) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
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
