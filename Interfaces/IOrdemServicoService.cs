using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IOrdemServicoService
    {
        public List<OrdemServico> GetAllOrdemServicos();
        public OrdemServico GetOrdemServicoByID(int ID);
        public void CreateNewOrdemServico(OrdemServico ordemServico);
        public void UpdateOrdemServico(OrdemServico ordemServico);
    }
}
