using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using PoolPiscinas.Services;
using System;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PoolPiscinas.Controllers
{
    public class PiscineiroController : Controller
    {
        private IUsuarioService _usuarioService;
        private IFranquiaService _franquiaService;
        private IFranqueadoService _franqueadoService;
        private IPiscineiroService _piscineiroService;

        public PiscineiroController(IUsuarioService usuarioService,
                                    IFranquiaService franquiaService,
                                    IFranqueadoService franqueadoService,
                                    IPiscineiroService piscineiroService)
        {
            _usuarioService = usuarioService;
            _franquiaService = franquiaService;
            _franqueadoService = franqueadoService;
            _piscineiroService = piscineiroService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Falta Enumerador
            var piscineiros = _usuarioService.GetAllUsuarios()
                .Where(x => x.Role.Nome == "Piscineiro");

            ViewBag.Piscineiros = piscineiros;

            return View();
        }

        //Garantir que essa rota seja acessada apenas pelo Franqueado logado
        [HttpGet]
        public IActionResult IndexPiscineirosFranqueado()
        {
            try
            {
                //obter franqueado
                var usuarioID = HttpContext.Session.GetInt32("UsuarioID");

                var franqueado = _franqueadoService.GetAllFranqueados()
                    .FirstOrDefault(x => x.UsuarioID == usuarioID);

                if (franqueado != null)
                {
                    var franquia = _franquiaService.GetFranquiaByID(franqueado.FranquiaID);

                    //Falta Enumerador
                    var piscineiros = _piscineiroService.GetAllPiscineiros()
                        .Where(x => x.FranquiaID == franquia.FranquiaID)
                        .ToList();

                    ViewBag.Piscineiros = piscineiros;
                }

                return View();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao obter os Piscineiros deste franqueado!");
            }
        }

        [HttpGet]
        public IActionResult GetByID(int PiscineiroID)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            

            return View();
        }

        [HttpPost]
        public IActionResult Create(Piscineiro Piscineiro)
        {
            try
            {
                var usuarioID = HttpContext.Session.GetInt32("UsuarioID");
                var usuarioLogado = _usuarioService.GetUsuarioByID((int)usuarioID);

                //Necessário enumerador
                var usuario = new Usuario()
                {
                    RoleID = 2,
                    Nome = Piscineiro.Usuario.Nome,
                    CPF = Piscineiro.Usuario.CPF,
                    CNPJ = Piscineiro.Usuario.CNPJ,
                    Senha = "123",
                    Ativo = true,
                    Email = Piscineiro.Usuario.Email
                };

                _usuarioService.CreateNewUser(usuario);

                Piscineiro.UsuarioID = usuario.UsuarioID;
                Piscineiro.Usuario = null;

                _piscineiroService.CreateNewPiscineiro(Piscineiro);

                TempData["SuccessMessage"] = "Registro efetuado com sucesso!";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao efetuar o registro!";
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            //Falta Enumerador
            var piscineiros = _usuarioService.GetAllUsuarios()
                .Where(x => x.Role.Nome == "Piscineiro");

            ViewBag.Piscineiros = piscineiros;

            return View();
        }

        [HttpPost]
        public IActionResult Edit(int PiscineiroID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int PiscineiroID)
        {
            return View();
        }
    }
}
