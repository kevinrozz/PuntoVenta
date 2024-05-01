using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVentaPresentacion.Web.Controllers
{
    public class VentasController : Controller
    {
        private readonly IVentaDominio _ventaDomain;
        public VentasController(IVentaDominio ventaDomain)
        {
            _ventaDomain = ventaDomain;
        }

        // GET: VentasController
        public ActionResult Index()
        {
            var items = _ventaDomain.GetAllVentas();

            if (items.IsSuccess)
                return View(items.Data);

            return View();
        }

        public ActionResult IndexUser()
        {
            if (!User.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(HttpContext.Session.GetString("IdUsuario"))) return RedirectToAction("Logout", "Home");

            Guid userId = new Guid(HttpContext.Session.GetString("IdUsuario"));

            var items = _ventaDomain.GetAllVentasByUsuario(userId);

            if (items.IsSuccess)
            {
                var itemsOrdered = items.Data.OrderByDescending(t => t.Consecutivo);
                return View(itemsOrdered);
            }

            return View();
        }

        // GET: VentasController/Details/5
        public ActionResult Details(Guid id)
        {
            var usuario = _ventaDomain.GetVenta(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View(usuario);
        }

        // GET: VentasController/Create
        public ActionResult Create()
        {
            var objVenta = new Venta();
            return View(objVenta);
        }

        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venta ventaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ventaModel.IdCaja = new Guid(HttpContext.Session.GetString("IdCurrentCaja"));
                    var resultCreate = _ventaDomain.CreateVenta(ventaModel);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(Create));
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear la venta.");
                }
            }

            return View(ventaModel);
        }

        // GET: VentasController/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            var usuario = _ventaDomain.GetVenta(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View();
        }

        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Venta ventaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _ventaDomain.UpdateVenta(ventaModel);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar la venta.");
                }
            }

            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            return View(ventaModel);
        }

        // GET: VentasController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var venta = _ventaDomain.GetVenta(id);

            if (venta.IsSuccess)
                return View(venta.Data);

            return View();
        }
    }
}