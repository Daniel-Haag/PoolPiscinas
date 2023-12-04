using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IRoleService
    {
        public List<Role> GetAllRoles();
        public Role GetRoleByID(int ID);
        public void CreateNewRole(Role role);
        public void UpdateRole(Role role);
    }
}
