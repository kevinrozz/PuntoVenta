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

namespace PuntoVentaPresentacion.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUsuarioDominio _userDomain;


		public HomeController(ILogger<HomeController> logger, IUsuarioDominio userDomain)
		{
			_logger = logger;
            _userDomain = userDomain;
		}

		public IActionResult Index()
		{
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return View();
                }
                else
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
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, resultLogin.Data.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, resultLogin.Data.UserName));

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index");
                }
            }

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
