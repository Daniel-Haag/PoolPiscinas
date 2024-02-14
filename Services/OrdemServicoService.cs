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
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public OrdemServicoService(PoolPiscinasDbContext poolPiscinasDbContext) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public List<OrdemServico> GetAllOrdemServicos()
        {
            return _poolPiscinasDbContext.OrdemServicos
                .Include(x => x.Agenda)
                .Include(x => x.Cliente)
                .ToList();
        }

        public OrdemServico GetOrdemServicoByID(int ID)
        {
            return _poolPiscinasDbContext.OrdemServicos
                .Include(x => x.Agenda)
                .Include(x => x.Cliente)
                .FirstOrDefault();
        }

        public void CreateNewOrdemServico(OrdemServico OrdemServico)
        {
            try
            {
                _poolPiscinasDbContext.OrdemServicos.Add(OrdemServico);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateOrdemServico(OrdemServico OrdemServico)
        {
            try
            {
                _poolPiscinasDbContext.OrdemServicos.Update(OrdemServico);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteOrdemServico(int ID)
        {
            try
            {
                var OrdemServico = GetOrdemServicoByID(ID);
                _poolPiscinasDbContext.OrdemServicos.Remove(OrdemServico);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
