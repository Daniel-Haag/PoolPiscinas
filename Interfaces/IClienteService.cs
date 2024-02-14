using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IClienteService
    {
        public List<Cliente> GetAllClientes();
        public Cliente GetClienteByID(int ID);
        public void CreateNewCliente(Cliente Cliente);
        public void UpdateCliente(Cliente Cliente);
    }
}
