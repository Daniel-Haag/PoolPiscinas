using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoolPiscinas.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PoolPiscinas.Sessions;
using Microsoft.AspNetCore.Http;
using PoolPiscinas.Interfaces;

namespace PoolPiscinas.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private ISessionUsuarioService _sessionUsuarioService;
        private ILoginService _loginService;
        private IUsuarioService _usuarioService;
        private IRoleService _roleService;
        private IFranquiaService _franquiaService;
        private IFranqueadoService _franqueadoService;

        public LoginController(ILogger<LoginController> logger,
                               ISessionUsuarioService sessionUsuarioService,
                               ILoginService loginService,
                               IUsuarioService usuarioService,
                               IRoleService roleService,
                               IFranquiaService franquiaService,
                               IFranqueadoService franqueadoService)
        {
            _logger = logger;
            _sessionUsuarioService = sessionUsuarioService;
            _loginService = loginService;
            _usuarioService = usuarioService;
            _roleService = roleService;
            _franquiaService = franquiaService;
            _franqueadoService = franqueadoService;
        }

        public IActionResult Login()
        {
            ViewBag.Funcoes = _roleService.GetAllRoles();
            ViewBag.Franquias = _franquiaService.GetAllFranquias();
            ViewBag.Franqueados = _franqueadoService.GetAllFranqueados();

            _loginService.RemoveSession(HttpContext);
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuarioLogin)
        {
            var usuario = _sessionUsuarioService.Login(usuarioLogin);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Nome", usuario.Nome);
                HttpContext.Session.SetString("Role", usuario.Role.Nome);
                HttpContext.Session.SetInt32("UsuarioID", usuario.UsuarioID);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa inválida de login.");
            }

            //-----------------------------------------------------------------------------

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome), // substituir por um nome de usuário real
                    //Adicionar outras claims aqui, se necessário
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _loginService.RemoveSession(HttpContext);
            return RedirectToAction("Login", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação do modelo."
                });
            }

            var role = _roleService.GetRoleByID(usuario.RoleID);

            _usuarioService.CreateNewUser(usuario);

            //Definir enumerador para roles
            if (role.RoleID == 1)
            {

            }
            else if (role.RoleID == 2)
            {

            }
            else if (role.RoleID == 3)
            {
                //var franquia = _franquiaService.GetFranquiaByID(usuario.f)

                //var franqueado = new Franqueado()
                //{
                //    UsuarioID = usuario.UsuarioID,
                //    FranquiaID = 
                //};

                //_franqueadoService.CreateNewFranqueado();
            }

            return Json(new ApiResponse
            {
                Success = true,
                Message = "Usuário cadastrado com sucesso."
            });
        }
    }
}
