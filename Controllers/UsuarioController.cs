using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Models;

namespace PoolPiscinas.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
