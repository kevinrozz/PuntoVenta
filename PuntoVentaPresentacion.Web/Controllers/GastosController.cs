using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVentaPresentacion.Web.Controllers
{
    public class GastosController : Controller
    {
        private readonly IGastoDominio _cateDominio;
        public GastosController(IGastoDominio cateDominio)
        {
            _cateDominio = cateDominio;
        }

        // GET: GastosController
        public ActionResult IndexUser()
        {
            if (!User.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(HttpContext.Session.GetString("IdUsuario"))) return RedirectToAction("Logout", "Home");

            Guid cajaId = new Guid(HttpContext.Session.GetString("IdCurrentCaja"));

            var items = _cateDominio.GetGastosByCaja(cajaId);

            if (items.IsSuccess)
                return View(items.Data);

            return View();
        }

        // GET: GastosController/Details/5
        public ActionResult Details(Guid id)
        {
            var usuario = _cateDominio.GetGasto(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View(usuario);
        }

        // GET: GastosController/Create
        public ActionResult Create()
        {
            var objGasto = new Gasto();
            return View(objGasto);
        }

        // POST: GastosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gasto gastoModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    gastoModel.IdCaja = new Guid(HttpContext.Session.GetString("IdCurrentCaja"));
                    var resultCreate = _cateDominio.CreateGasto(gastoModel);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(IndexUser));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el gasto.");
                }
            }

            return View(gastoModel);
        }

        // GET: GastosController/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            var usuario = _cateDominio.GetGasto(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View();
        }

        // POST: GastosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Gasto gastoModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _cateDominio.UpdateGasto(gastoModel);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(IndexUser));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el gasto.");
                }
            }

            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            return View(gastoModel);
        }

        // GET: GastosController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var gasto = _cateDominio.GetGasto(id);

            if (gasto.IsSuccess)
                return View(gasto.Data);

            return View();
        }

        // POST: GastosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Gasto gastoModel)
        {
            try
            {
                var resultCreate = _cateDominio.DeleteGasto(id);

                if (resultCreate.IsSuccess)
                    return RedirectToAction(nameof(IndexUser));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar el gasto.");
            }

            return View();
        }
    }
}