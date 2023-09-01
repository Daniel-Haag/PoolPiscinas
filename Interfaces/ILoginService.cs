using Microsoft.AspNetCore.Http;

namespace PoolPiscinas.Interfaces
{
    public interface ILoginService
    {
        public void RemoveSession(HttpContext HttpContext);
    }
}
