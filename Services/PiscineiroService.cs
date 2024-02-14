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
    public class PiscineiroService : IPiscineiroService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;
        private readonly IUsuarioService _usuarioService;

        public PiscineiroService(PoolPiscinasDbContext poolPiscinasDbContext,
            IUsuarioService usuarioService) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
            _usuarioService = usuarioService;
        }

        public List<Piscineiro> GetAllPiscineiros()
        {
            return _poolPiscinasDbContext.Piscineiros
                .Include(x => x.Usuario)
                .Include(x => x.Franquia)
                .Where(x => x.Usuario.Ativo)
                .ToList();
        }

        public Piscineiro GetPiscineiroByID(int ID)
        {
            return _poolPiscinasDbContext.Piscineiros
                .Include(x => x.Usuario)
                .Include(x => x.Franquia)
                .Where(x => x.Usuario.Ativo)
                .FirstOrDefault();
        }

        public void CreateNewPiscineiro(Piscineiro Piscineiro)
        {
            try
            {
                _poolPiscinasDbContext.Piscineiros.Add(Piscineiro);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdatePiscineiro(Piscineiro Piscineiro)
        {
            try
            {
                _poolPiscinasDbContext.Piscineiros.Update(Piscineiro);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeletePiscineiro(int ID)
        {
            try
            {
                var piscineiro = GetPiscineiroByID(ID);
                var usuario = _usuarioService.GetUsuarioByID(piscineiro.UsuarioID);

                usuario.Ativo = false;

                _poolPiscinasDbContext.Usuarios.Update(usuario);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
