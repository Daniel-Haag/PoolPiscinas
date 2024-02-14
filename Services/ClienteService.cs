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
    public class ClienteService : IClienteService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public ClienteService(PoolPiscinasDbContext poolPiscinasDbContext)
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public List<Cliente> GetAllClientes()
        {
            return _poolPiscinasDbContext.Clientes
                .Include(x => x.Usuario)
                .Include(x => x.Piscineiro)
                .Where(x => x.Usuario.Ativo)
                .ToList();
        }

        public Cliente GetClienteByID(int ID)
        {
            return _poolPiscinasDbContext.Clientes
                .Include(x => x.Usuario)
                .Include(x => x.Piscineiro)
                .Where(x => x.Usuario.Ativo)
                .FirstOrDefault();
        }

        public void CreateNewCliente(Cliente cliente)
        {
            try
            {
                _poolPiscinasDbContext.Clientes.Add(cliente);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateCliente(Cliente cliente)
        {
            try
            {
                _poolPiscinasDbContext.Clientes.Update(cliente);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteCliente(int ID)
        {
            try
            {
                var Cliente = GetClienteByID(ID);
                Cliente.Usuario.Ativo = false;
                _poolPiscinasDbContext.Clientes.Update(Cliente);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
