using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IFranquiaService
    {
        public List<Franquia> GetAllFranquias();
        public Franquia GetFranquiaByID(int ID);
        public void CreateNewFranquia(Franquia franquia);
        public void UpdateFranquia(Franquia franquia);
    }
}
