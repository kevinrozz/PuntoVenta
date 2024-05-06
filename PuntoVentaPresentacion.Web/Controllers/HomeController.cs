using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PuntoVenta.Dominio.Entity;
using PuntoVentaPresentacion.Web.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVentaPresentacion.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioDominio _userDomain;
        private readonly ICajaDominio _cajaDomain;

        public HomeController(ILogger<HomeController> logger, IUsuarioDominio userDomain, ICajaDominio cajaDomain)
		{
			_logger = logger;
            _userDomain = userDomain;
            _cajaDomain = cajaDomain;
		}

		public IActionResult Index()
		{
            try
            {
                if (User.Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("IdUsuario")))
                    return View();

                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            var account = new CuentaAuthLogin();
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CuentaAuthLogin obj)
        {
            if (ModelState.IsValid)
            {
                var resultLogin = _userDomain.Login(obj.UserName, obj.Password);

                if (resultLogin.IsSuccess)
                {
                    if (new List<EnumEstadosUsuario>() { EnumEstadosUsuario.Activo, EnumEstadosUsuario.Operando }.Contains(resultLogin.Data.IdEstado))
                    {
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, resultLogin.Data.Id.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Name, resultLogin.Data.UserName));

                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("IdUsuario", resultLogin.Data.Id.ToString());
                        HttpContext.Session.SetString("IdRol", resultLogin.Data.IdRol.ToString());
                        HttpContext.Session.SetString("IdEstado", resultLogin.Data.IdEstado.ToString());

                        if (resultLogin.Data.IdEstado == EnumEstadosUsuario.Operando)
                        {
                            var resultCajaAbierta = _cajaDomain.GetCajaAbiertaByUser(resultLogin.Data.Id);

                            if (resultCajaAbierta.IsSuccess && resultCajaAbierta.Data != null)
                                HttpContext.Session.SetString("IdCurrentCaja", resultCajaAbierta.Data.Id.ToString());
                        }
                        return View("Index");
                    }
                    else
                    {
                        TempData["typeAlert"] = "danger";
                        TempData["messageAlert"] = $"El usuario esta {resultLogin.Data.IdEstado}";
                        return View();
                    }
                }
            }

            TempData["typeAlert"] = "danger";
            TempData["messageAlert"] = "Usuario y/o credenciales invalidas";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            TempData["alert"] = null;
            TempData["currentAccountId"] = null;

            return RedirectToAction("Index");
        }
    }
}
