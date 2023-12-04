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
    public class FranqueadoService : IFranqueadoService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public FranqueadoService(PoolPiscinasDbContext poolPiscinasDbContext) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public List<Franqueado> GetAllFranqueados()
        {
            return _poolPiscinasDbContext.Franqueados
                .Include(x => x.Usuario)
                .Include(x => x.Franquia)
                .ToList();
        }

        public Franqueado GetFranqueadoByID(int ID)
        {
            return _poolPiscinasDbContext.Franqueados
                .Include(x => x.Usuario)
                .Include(x => x.Franquia)
                .FirstOrDefault();
        }

        public void CreateNewFranqueado(Franqueado Franqueado)
        {
            try
            {
                _poolPiscinasDbContext.Franqueados.Add(Franqueado);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateFranqueado(Franqueado Franqueado)
        {
            try
            {
                _poolPiscinasDbContext.Franqueados.Update(Franqueado);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteFranqueado(int ID)
        {
            try
            {
                var Franqueado = GetFranqueadoByID(ID);
                _poolPiscinasDbContext.Franqueados.Remove(Franqueado);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
