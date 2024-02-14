using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;
using System.Collections.Generic;

namespace PoolPiscinas.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> GetAllUsuarios();
        Usuario GetUsuarioByID(int ID);
        Usuario GetUsuarioFranqueadoByID(int ID);
        Usuario GetUsuarioByCPForCNPJandSenha(string CPF, string CNPJ, string senha);
        void CreateNewUser(Usuario usuario);
    }
}
