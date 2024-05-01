using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVentaPresentacion.Web.Controllers
{
    public class CajasController : Controller
    {
        private readonly ICajaDominio _cajaDomain;
        private readonly IUsuarioDominio _userDomain;
        public CajasController(ICajaDominio cajaDomain, IUsuarioDominio userDomain)
        {
            _cajaDomain = cajaDomain;
            _userDomain = userDomain;
        }

        // GET: CajasController
        public ActionResult Index()
        {
            var items = _cajaDomain.GetAllCajas();

            if (items.IsSuccess)
                return View(items.Data);

            return View();
        }

        // GET: CajasController
        public ActionResult IndexUser()
        {
            if (!User.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(HttpContext.Session.GetString("IdUsuario"))) return RedirectToAction("Logout", "Home");
            
            Guid userId = new Guid(HttpContext.Session.GetString("IdUsuario"));

            var items = _cajaDomain.GetCajasByUsuario(userId);

            if (items.IsSuccess)
            {
                var itemsOrdered = items.Data.OrderByDescending(t => t.FechaApertura);
                return View(itemsOrdered);
            }

            return View();
        }

        // GET: CajasController/Details/5
        public ActionResult Details(Guid id)
        {
            var usuario = _cajaDomain.GetCaja(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View(usuario);
        }

        // GET: CajasController/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(HttpContext.Session.GetString("IdUsuario"))) return RedirectToAction("Logout", "Home");

            var objCaja = new Caja();
            return View(objCaja);
        }

        // POST: CajasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Caja cajaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = _userDomain.GetUsuario(new Guid(HttpContext.Session.GetString("IdUsuario")));

                    if (currentUser.IsSuccess)
                    {
                        cajaModel.IdUsuario = currentUser.Data.Id;
                        currentUser.Data.IdEstado = EnumEstadosUsuario.Operando;
                        var resultCreate = _cajaDomain.AbrirCaja(cajaModel);

                        if (resultCreate.IsSuccess)
                        {
                            HttpContext.Session.SetString("IdCurrentCaja", cajaModel.Id.ToString());
                            HttpContext.Session.SetString("IdEstado", EnumEstadosUsuario.Operando.ToString());
                            return RedirectToAction(nameof(Create), "Ventas");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al abrir la caja.");
                }
            }

            return View(cajaModel);
        }

        // GET: CajasController/Close/5
        public ActionResult Close()
        {
            var caja = _cajaDomain.GetCaja(new Guid(HttpContext.Session.GetString("IdCurrentCaja")));

            if (caja.IsSuccess)
            {
                var cajaCalculada = _cajaDomain.GetTotales(caja.Data.Id);
                return View(cajaCalculada.Data);
            }

            return View();
        }

        // POST: CajasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Close(Guid id, Caja cajaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _cajaDomain.CerrarCaja(cajaModel);

                    if (resultCreate.IsSuccess)
                    {
                        HttpContext.Session.SetString("IdEstado", EnumEstadosUsuario.Activo.ToString());
                        return RedirectToAction(nameof(IndexUser));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar la caja.");
                }
            }

            return View(cajaModel);
        }
    }
}
