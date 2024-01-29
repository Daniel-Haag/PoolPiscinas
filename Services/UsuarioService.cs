using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public Usuario GetUsuarioByID(int ID)
        {
            try
            {
                var usuario = _poolPiscinasDbContext.Usuarios
                    .Include(x => x.Role)
                    .FirstOrDefault(x => x.UsuarioID == ID);

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter usuário!");
            }
        }

        public Usuario GetUsuarioByCPForCNPJandSenha(string CPF, string CNPJ, string senha)
        {
            if (!string.IsNullOrEmpty(CPF))
            {
                var usuario = _poolPiscinasDbContext.Usuarios
                                .Include(x => x.Role)
                                .FirstOrDefault(x => x.CPF == CPF && senha == x.Senha && x.Ativo);

                return usuario;
            }
            else if (!string.IsNullOrEmpty(CNPJ))
            {
                var usuario = _poolPiscinasDbContext.Usuarios
                                .Include(x => x.Role)
                                .FirstOrDefault(x => x.CNPJ == CNPJ && senha == x.Senha && x.Ativo);

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
