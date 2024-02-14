using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using System;
using System.Linq;
using System.Net.WebSockets;

namespace PoolPiscinas.Controllers
{
    public class FranquiaController : Controller
    {
        private IFranquiaService _franquiaService;
        private IUsuarioService _usuarioService;
        private IFranqueadoService _franqueadoService;

        public FranquiaController(IFranquiaService franquiaService,
                                  IUsuarioService usuarioService,
                                  IFranqueadoService franqueadoService)
        {
            _franquiaService = franquiaService;
            _usuarioService = usuarioService;
            _franqueadoService = franqueadoService;
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
                //Se for franqueado, só pode cadastrar uma por vez
                var usuarioID = HttpContext.Session.GetInt32("UsuarioID");
                var usuario = _usuarioService.GetUsuarioByID((int)usuarioID);

                //Incluir enumerador
                if (usuario.Role.Nome == "Franqueado")
                {
                    var franqueado = _franqueadoService.GetAllFranqueados()
                        .FirstOrDefault(x => x.UsuarioID == usuario.UsuarioID);

                    if (franqueado == null)
                    {
                        _franquiaService.CreateNewFranquia(franquia);

                        Franqueado novoFranqueado = new Franqueado()
                        {
                            UsuarioID = usuario.UsuarioID,
                            Usuario = usuario,
                            FranquiaID = franquia.FranquiaID,
                            Franquia = franquia
                        };

                        _franqueadoService.CreateNewFranqueado(novoFranqueado);

                        TempData["SuccessMessage"] = "Registro efetuado com sucesso!";
                        return View();
                    }
                    else if (franqueado != null)
                    {
                        var franquiaExistente = _franquiaService.GetFranquiaByID(franqueado.FranquiaID);

                        if (franquiaExistente != null)
                        {
                            TempData["ErrorMessage"] = "Franquia já existente!";
                            return View();
                        }
                        else
                        {
                            _franquiaService.CreateNewFranquia(franquia);
                            TempData["SuccessMessage"] = "Registro efetuado com sucesso!";
                            return View();
                        }
                    }
                }
                _franquiaService.CreateNewFranquia(franquia);
                TempData["SuccessMessage"] = "Registro efetuado com sucesso!";
                return View();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao efetuar o registro!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            TempData["SuccessMessage"] = null;
            ViewBag.Franquia = _franquiaService.GetFranquiaByID(ID);
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
