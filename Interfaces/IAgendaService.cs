using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IAgendaService
    {
        public List<Agenda> GetAllAgendas();
        public Agenda GetAgendaByID(int ID);
        public void CreateNewAgenda(Agenda Agenda);
        public void UpdateAgenda(Agenda Agenda);
        public List<Agenda> GetAgendaByMonthYear(int month, int year);
    }
}
