using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Models;

namespace PoolPiscinas.Controllers
{
    public class FranqueadoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetByID(int franqueadoID)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Franqueado franqueado)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int franqueadoID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int franqueadoID)
        {
            return View();
        }
    }
}
