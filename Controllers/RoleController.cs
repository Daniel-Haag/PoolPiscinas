using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Models;

namespace PoolPiscinas.Controllers
{
    public class RoleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetByID(int roleID)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int roleID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int roleID)
        {
            return View();
        }
    }
}
