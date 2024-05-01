using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVentaPresentacion.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaDominio _cateDominio;
        public CategoriasController(ICategoriaDominio cateDominio)
        {
            _cateDominio = cateDominio;
        }

        // GET: CategoriasController
        public ActionResult Index()
        {
            var items = _cateDominio.GetCategorias();

            if (items.IsSuccess)
                return View(items.Data);

            return View();
        }

        // GET: CategoriasController/Details/5
        public ActionResult Details(Guid id)
        {
            var usuario = _cateDominio.GetCategoria(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View(usuario);
        }

        // GET: CategoriasController/Create
        public ActionResult Create()
        {
            var objCategoria = new Categoria();
            return View(objCategoria);
        }

        // POST: CategoriasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoriaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _cateDominio.CreateCategoria(categoriaModel);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear la categoria.");
                }
            }

            return View(categoriaModel);
        }

        // GET: CategoriasController/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            var usuario = _cateDominio.GetCategoria(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View();
        }

        // POST: CategoriasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Categoria categoriaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _cateDominio.UpdateCategoria(categoriaModel);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar la categoria.");
                }
            }

            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            return View(categoriaModel);
        }

        // GET: CategoriasController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var categoria = _cateDominio.GetCategoria(id);

            if (categoria.IsSuccess)
                return View(categoria.Data);

            return View();
        }

        // POST: CategoriasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Categoria categoriaModel)
        {
            try
            {
                var resultCreate = _cateDominio.DeleteCategoria(id);

                if (resultCreate.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar la categoria.");
            }

            return View();
        }
    }
}