using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolPiscinas.Services
{
    public class RoleService : IRoleService
    {
        private readonly PoolPiscinasDbContext _poolPiscinasDbContext;

        public RoleService(PoolPiscinasDbContext poolPiscinasDbContext) 
        {
            _poolPiscinasDbContext = poolPiscinasDbContext;
        }

        public List<Role> GetAllRoles()
        {
            return _poolPiscinasDbContext.Roles
                .ToList();
        }

        public Role GetRoleByID(int ID)
        {
            return _poolPiscinasDbContext.Roles
                .FirstOrDefault();
        }

        public void CreateNewRole(Role role)
        {
            try
            {
                _poolPiscinasDbContext.Roles.Add(role);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateRole(Role role)
        {
            try
            {
                _poolPiscinasDbContext.Roles.Update(role);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteRole(int ID)
        {
            try
            {
                var role = GetRoleByID(ID);
                _poolPiscinasDbContext.Roles.Remove(role);
                _poolPiscinasDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
