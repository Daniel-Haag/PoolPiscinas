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

        public LoginController(ILogger<LoginController> logger,
                               ISessionUsuarioService sessionUsuarioService,
                               ILoginService loginService,
                               IUsuarioService usuarioService)
        {
            _logger = logger;
            _sessionUsuarioService = sessionUsuarioService;
            _loginService = loginService;
            _usuarioService = usuarioService;
        }

        public IActionResult Login()
        {
            _loginService.RemoveSession(HttpContext);
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if ((loginModel.CPF == "111.111.111-11" || loginModel.CNPJ == "11.111.111/1111-11") && loginModel.Senha == "password")
            {
                var usuarioLogando = new Usuario
                {
                    Nome = "Usuario 1",
                    Senha = "123",
                    Role = new Role()
                    {
                        RoleID = 1,
                        Nome = "Franqueado",
                        Ativo = true
                    }
                };

                var usuario = _sessionUsuarioService.Login(usuarioLogando);

                if (usuario != null)
                {
                    HttpContext.Session.SetString("Nome", usuario.Nome);
                    HttpContext.Session.SetString("Role", usuario.Role.Nome);
                    return RedirectToAction("Index", "Home");
                }

                //-----------------------------------

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
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa inválida de login.");
                return View();
            }
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
        public IActionResult CreateUsuario([FromBody]Usuario usuario)
        {
            _usuarioService.CreateNewUser(usuario);
            return Ok(new { success = true, message = "Usuário criado com sucesso" });
        }
    }
}
