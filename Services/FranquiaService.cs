using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolPiscinas.Services
{
    public class FranquiaService : IFranquiaService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public FranquiaService(PoolPiscinasDbContext poolPiscinasDbContext) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public List<Franquia> GetAllFranquias()
        {
            return _poolPiscinasDbContext.Franquias
                .ToList();
        }

        public Franquia GetFranquiaByID(int ID)
        {
            return _poolPiscinasDbContext.Franquias
                .FirstOrDefault();
        }

        public void CreateNewFranquia(Franquia franquia)
        {
            try
            {
                _poolPiscinasDbContext.Franquias.Add(franquia);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateFranquia(Franquia franquia)
        {
            try
            {
                _poolPiscinasDbContext.Franquias.Update(franquia);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteFranquia(int ID)
        {
            try
            {
                var franquia = GetFranquiaByID(ID);
                _poolPiscinasDbContext.Franquias.Remove(franquia);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
