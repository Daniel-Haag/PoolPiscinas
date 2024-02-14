using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoolPiscinas.Interfaces;
using PoolPiscinas.Models;
using PoolPiscinas.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PoolPiscinas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFranquiaService _franquiaService;
        private IOrdemServicoService _ordemServicoService;
        private IUsuarioService _usuarioService;
        private IClienteService _clienteService;
        private IPiscineiroService _piscineiroService;
        private IFranqueadoService _franqueadoService;
        private IAgendaService _agendaService;

        public HomeController(ILogger<HomeController> logger,
                              IFranquiaService franquiaService,
                              IOrdemServicoService ordemServicoService,
                              IUsuarioService usuarioService,
                              IClienteService clienteService,
                              IPiscineiroService piscineiroService,
                              IFranqueadoService franqueadoService,
                              IAgendaService agendaService)
        {
            _logger = logger;
            _franquiaService = franquiaService;
            _ordemServicoService = ordemServicoService;
            _usuarioService = usuarioService;
            _clienteService = clienteService;
            _piscineiroService = piscineiroService;
            _franqueadoService = franqueadoService;
            _agendaService = agendaService;
        }

        public IActionResult Index(int? month, int? year)
        {
            var usuarioID = HttpContext.Session.GetInt32("UsuarioID");
            var usuario = _usuarioService.GetUsuarioByID((int)usuarioID);

            DateTime currentDate = DateTime.Now;

            if (month.HasValue && year.HasValue)
            {
                currentDate = new DateTime(year.Value, month.Value, 1);

                var registros = _agendaService.GetAgendaByMonthYear(month.Value, year.Value);

                var diasIniciais = registros.Select(x => x.DataInicial.Day).ToList();
                var diasFinais = registros.Select(x => x.DataFinal.Day).ToList();

                List<int> dias = new List<int>();
                dias.AddRange(diasIniciais);
                dias.AddRange(diasFinais);

                ViewBag.DiasComRegistros = dias;

                // Tratar casos onde o mês ou ano excedem os limites
                if (currentDate.Month == 0)
                {
                    currentDate = currentDate.AddYears(-1).AddMonths(12);
                }
                else if (currentDate.Month == 13)
                {
                    currentDate = currentDate.AddYears(1).AddMonths(-12);
                }
            }
            else
            {
                var registros = _agendaService.GetAgendaByMonthYear(currentDate.Month, currentDate.Year);

                var diasIniciais = registros.Select(x => x.DataInicial.Day).ToList();
                var diasFinais = registros.Select(x => x.DataFinal.Day).ToList();

                List<int> dias = new List<int>();
                dias.AddRange(diasIniciais);
                dias.AddRange(diasFinais);

                ViewBag.DiasComRegistros = dias;
            }

            ViewBag.CurrentDate = currentDate;

            if (usuario.Role.Nome == "Franqueado")
            {
                var franqueado = _franqueadoService.GetAllFranqueados()
                    .FirstOrDefault(x => x.UsuarioID == usuarioID);

                if (franqueado != null)
                {
                    //obtem as OS's dos seus piscineiros
                    //primeiro obtem os clientes
                    //eu posso saber quem são meus clientes, através do piscineiro
                    var franquia = _franquiaService.GetFranquiaByID(franqueado.FranquiaID);

                    var piscineirosDaFranquia = _piscineiroService.GetAllPiscineiros()
                        .Where(x => x.FranquiaID == franquia.FranquiaID)
                        .ToList();

                    var piscineirosIDs = piscineirosDaFranquia.Select(x => x.PiscineiroID);

                    //O cliente está ligado à uma franquia, porém através do seu respectivo piscineiro
                    var clientesDosPiscineiros = _clienteService.GetAllClientes()
                        .Where(x => piscineirosIDs.Contains(x.PiscineiroID))
                        .ToList();

                    //Preciso ter clientesID
                    var clientesIDs = clientesDosPiscineiros.Select(x => x.ClienteID);

                    //Finalmente as ordens de serviço respectivas ao franqueado em questão
                    var ordensServico = _ordemServicoService.GetAllOrdemServicos()
                        .Where(x => clientesIDs.Contains(x.ClienteID));

                    ViewBag.Franquias = _franquiaService.GetAllFranquias();
                    ViewBag.OrdensServico = ordensServico;
                }
            }
            else if (usuario.Role.Nome == "Piscineiro")
            {
                var piscineiro = _piscineiroService.GetPiscineiroByID((int)usuarioID);

                if (piscineiro != null)
                {
                    //obter os clientes deste piscineiro
                    var clientesDoPiscineiro = _clienteService.GetAllClientes()
                        .Where(x => x.PiscineiroID == piscineiro.PiscineiroID);

                    var clientesDoPiscineiroIDs = clientesDoPiscineiro.Select(x => x.ClienteID);

                    //obtem apenas as suas OS's
                    //Finalmente as ordens de serviço respectivas ao piscineiro em questão
                    var ordensServico = _ordemServicoService.GetAllOrdemServicos()
                        .Where(x => clientesDoPiscineiroIDs.Contains(x.ClienteID));

                    ViewBag.Franquias = _franquiaService.GetAllFranquias();
                    ViewBag.OrdensServico = ordensServico;
                }
            }

            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
