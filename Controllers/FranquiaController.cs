using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using System;

namespace PoolPiscinas.Controllers
{
    public class FranquiaController : Controller
    {
        private IFranquiaService _franquiaService;

        public FranquiaController(IFranquiaService franquiaService)
        {
            _franquiaService = franquiaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Franquias = _franquiaService.GetAllFranquias();
            return View();
        }

        [HttpGet]
        public IActionResult GetByID()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            TempData["SuccessMessage"] = null;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Franquia franquia)
        {
            try
            {
                _franquiaService.CreateNewFranquia(franquia);
                TempData["SuccessMessage"] = "Registro efetuado com sucesso!";
                return View();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int franquiaID)
        {
            TempData["SuccessMessage"] = null;
            ViewBag.Franquia = _franquiaService.GetFranquiaByID(franquiaID);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Franquia franquia)
        {
            try
            {
                _franquiaService.UpdateFranquia(franquia);
                TempData["SuccessMessage"] = "Registro alterado com sucesso!";
                ViewBag.Franquia = franquia;
                return View();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(int franquiaID)
        {
            return View();
        }
    }
}
