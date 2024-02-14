using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using PoolPiscinas.Services;
using System.Linq;
using System;

namespace PoolPiscinas.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteService _clienteService;
        private IUsuarioService _usuarioService;
        private IFranquiaService _franquiaService;
        private IFranqueadoService _franqueadoService;
        private IPiscineiroService _piscineiroService;

        public ClienteController(IClienteService clienteService,
                                 IUsuarioService usuarioService,
                                 IFranquiaService franquiaService,
                                 IFranqueadoService franqueadoService,
                                 IPiscineiroService piscineiroService)
        {
            _clienteService = clienteService;
            _usuarioService = usuarioService;
            _franquiaService = franquiaService;
            _franqueadoService = franqueadoService;
            _piscineiroService = piscineiroService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var usuarioID = HttpContext.Session.GetInt32("UsuarioID");
            var usuario = _usuarioService.GetUsuarioByID((int)usuarioID);

            if (usuario != null)
            {
                if (usuario.Role.Nome == "Franqueado")
                {
                    var franqueado = _franqueadoService.GetAllFranqueados()
                        .FirstOrDefault(x => x.UsuarioID == usuario.UsuarioID);

                    var franquia = _franquiaService.GetFranquiaByID(franqueado.FranquiaID);

                    //obter os piscineiros deste franqueado
                    var piscineirosDaFranquia = _piscineiroService.GetAllPiscineiros()
                        .Where(x => x.FranquiaID == franquia.FranquiaID)
                        .ToList();

                    var piscineirosIDs = piscineirosDaFranquia.Select(x => x.PiscineiroID);

                    var clientesDestesPiscineiros = _clienteService.GetAllClientes()
                        .Where(x => piscineirosIDs.Contains(x.UsuarioID))
                        .ToList();

                    ViewBag.Clientes = clientesDestesPiscineiros;
                }

                ViewBag.Clientes = _clienteService.GetAllClientes();
            }

            return View();
        }

        [HttpGet]
        public IActionResult GetByID(int clienteID)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            TempData["SuccessMessage"] = null;
            ViewBag.Piscineiros = _piscineiroService.GetAllPiscineiros();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            try
            {
                //Necessário enumerador
                var usuario = new Usuario()
                {
                    RoleID = 1,
                    Nome = cliente.Usuario.Nome,
                    CPF = cliente.Usuario.CPF,
                    CNPJ = cliente.Usuario.CNPJ,
                    Senha = "123",
                    Ativo = true,
                    Email = cliente.Usuario.Email
                };

                _usuarioService.CreateNewUser(usuario);

                cliente.UsuarioID = usuario.UsuarioID;
                cliente.Usuario = null;

                _clienteService.CreateNewCliente(cliente);

                TempData["SuccessMessage"] = "Registro efetuado com sucesso!";
                ViewBag.Piscineiros = _piscineiroService.GetAllPiscineiros();
                return View();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int clienteID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int clienteID)
        {
            return View();
        }
    }
}
