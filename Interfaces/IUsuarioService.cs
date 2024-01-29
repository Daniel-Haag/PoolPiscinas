using Microsoft.AspNetCore.Http;
using PoolPiscinas.Models;

namespace PoolPiscinas.Interfaces
{
    public interface IUsuarioService
    {
        Usuario GetUsuarioByCPForCNPJandSenha(string CPF, string CNPJ, string senha);
        void CreateNewUser(Usuario usuario);
    }
}
