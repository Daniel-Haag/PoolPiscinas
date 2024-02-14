using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using System;
using System.Linq;

namespace PoolPiscinas.Controllers
{
    public class OrdemServicoController : Controller
    {
        private IUsuarioService _usuarioService;
        private IOrdemServicoService _ordemServicoService;
        private IClienteService _clienteService;
        private IAgendaService _agendaService;

        public OrdemServicoController(IUsuarioService usuarioService,
                                      IOrdemServicoService ordemServicoService,
                                      IClienteService clienteService,
                                      IAgendaService agendaService)
        {
            _usuarioService = usuarioService;
            _ordemServicoService = ordemServicoService;
            _clienteService = clienteService;
            _agendaService = agendaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetByID(int ordemServicoID)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.UsuariosResponsaveis = _usuarioService.GetAllUsuarios()
                .Where(x => x.Role.Nome == "Piscineiro");

            ViewBag.UsuariosClientes = _clienteService.GetAllClientes();

            return View();
        }

        [HttpPost]
        public IActionResult Create(OrdemServico ordemServico)
        {
            try
            {
                //Não esquecer de registrar na tabela Agenda
                var usuarioID = HttpContext.Session.GetInt32("UsuarioID");
                var usuario = _usuarioService.GetUsuarioByID((int)usuarioID);

                if (usuario != null)
                {
                    _ordemServicoService.CreateNewOrdemServico(ordemServico);

                    ViewBag.UsuariosResponsaveis = _usuarioService.GetAllUsuarios()
                    .Where(x => x.Role.Nome == "Piscineiro");
                    ViewBag.UsuariosClientes = _clienteService.GetAllClientes();

                    TempData["SuccessMessage"] = "Registro efetuado com sucesso!";
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao efetuar o registro!";
                ModelState.AddModelError("", "Erro ao registrar ordem de serviço!");
                //throw new Exception("Erro ao registrar ordem de serviço!");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int ordemServicoID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int ordemServicoID)
        {
            return View();
        }
    }
}
