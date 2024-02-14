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
    public class AgendaService : IAgendaService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public AgendaService(PoolPiscinasDbContext poolPiscinasDbContext)
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public List<Agenda> GetAllAgendas()
        {
            return _poolPiscinasDbContext.Agendas
                .Include(x => x.Usuario)
                .ToList();
        }

        public Agenda GetAgendaByID(int ID)
        {
            return _poolPiscinasDbContext.Agendas
                .Include(x => x.Usuario)
                .FirstOrDefault(x => x.AgendaID == ID);
        }

        public List<Agenda> GetAgendaByMonthYear(int month, int year)
        {
            return _poolPiscinasDbContext.Agendas
                .Include(x => x.Usuario)
                .Where(x => x.DataInicial.Month == month && x.DataFinal.Year == year)
                .ToList();
        }

        public void CreateNewAgenda(Agenda Agenda)
        {
            try
            {
                _poolPiscinasDbContext.Agendas.Add(Agenda);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateAgenda(Agenda Agenda)
        {
            try
            {
                _poolPiscinasDbContext.Agendas.Update(Agenda);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteAgenda(int ID)
        {
            try
            {
                var Agenda = GetAgendaByID(ID);
                _poolPiscinasDbContext.Agendas.Remove(Agenda);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
