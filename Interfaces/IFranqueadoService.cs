using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IFranqueadoService
    {
        public List<Franqueado> GetAllFranqueados();
        public Franqueado GetFranqueadoByID(int ID);
        public void CreateNewFranqueado(Franqueado Franqueado);
        public void UpdateFranqueado(Franqueado Franqueado);
    }
}
