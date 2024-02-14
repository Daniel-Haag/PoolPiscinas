using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IPiscineiroService
    {
        public List<Piscineiro> GetAllPiscineiros();
        public Piscineiro GetPiscineiroByID(int ID);
        public void CreateNewPiscineiro(Piscineiro Piscineiro);
        public void UpdatePiscineiro(Piscineiro Piscineiro);
    }
}
