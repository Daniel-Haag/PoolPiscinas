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

        public PiscineiroService(PoolPiscinasDbContext poolPiscinasDbContext) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public List<Piscineiro> GetAllPiscineiros()
        {
            return _poolPiscinasDbContext.Piscineiros
                .Include(x => x.Usuario)
                .Include(x => x.Franquia)
                .ToList();
        }

        public Piscineiro GetPiscineiroByID(int ID)
        {
            return _poolPiscinasDbContext.Piscineiros
                .Include(x => x.Usuario)
                .Include(x => x.Franquia)
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
                var Piscineiro = GetPiscineiroByID(ID);
                _poolPiscinasDbContext.Piscineiros.Remove(Piscineiro);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
