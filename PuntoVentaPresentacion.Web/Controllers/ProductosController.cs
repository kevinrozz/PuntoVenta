using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Transversal.Enums;
using PuntoVentaPresentacion.Web.Models.ViewModels;

namespace PuntoVentaPresentacion.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoDominio _prodDomain;
        private readonly ICategoriaDominio _cateDominio;
        public ProductosController(IProductoDominio prodDomain, ICategoriaDominio cateDominio)
        {
            _prodDomain = prodDomain;
            _cateDominio = cateDominio;

        }

        // GET: ProductosController
        public ActionResult Index()
        {
            var productos = _prodDomain.GetProductos();

            if (productos.IsSuccess)
            {
                return View(productos.Data);
            }

            return View();
        }

        // GET: ProductosController/Details/5
        public ActionResult Details(Guid id)
        {
            var producto = _prodDomain.GetProducto(id);

            if (producto.IsSuccess)
            {
                return View(producto.Data);
            }

            return View();
        }

        // GET: ProductosController/Create
        public ActionResult Create()
        {
            var respCategorias = _cateDominio.GetCategorias();

            if (respCategorias.IsSuccess)
            {
                var viewModel = new ProductoViewModel()
                {
                    Categorias = respCategorias.Data.Select(i => new SelectListItem
                    {
                        Text = i.Nombre.ToString(),
                        Value = i.Id.ToString()
                    }),
                    Producto = new Producto()
                };

                return View(viewModel);
            }

            return View();
        }

        // POST: ProductosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoViewModel productoViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _prodDomain.CreateProducto(productoViewModel.Producto);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(Index));
                    else
                        new Exception("Ocurrió un error al crear el producto.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var respCategorias = _cateDominio.GetCategorias();

            if (respCategorias.IsSuccess)
            {
                var viewModel = new ProductoViewModel()
                {
                    Categorias = respCategorias.Data.Select(i => new SelectListItem
                    {
                        Text = i.Nombre.ToString(),
                        Value = i.Id.ToString()
                    }),
                    Producto = productoViewModel.Producto
                };

                return View(viewModel);
            }

            return View();
        }

        // GET: ProductosController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var respCategorias = _cateDominio.GetCategorias();
            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            if (respCategorias.IsSuccess)
            {
                var viewModel = new ProductoViewModel()
                {
                    Categorias = respCategorias.Data.Select(i => new SelectListItem
                    {
                        Text = i.Nombre.ToString(),
                        Value = i.Id.ToString()
                    }),
                    Producto = _prodDomain.GetProducto(id).Data
                };

                return View(viewModel);
            }

            return View();
        }

        // POST: ProductosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ProductoViewModel productoViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productoViewModel.Producto.Id = id;
                    var resultCreate = _prodDomain.UpdateProducto(productoViewModel.Producto);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el producto.");
                }
            }

            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            return View(productoViewModel);
        }

        // GET: ProductosController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var producto = _prodDomain.GetProducto(id);

            if (producto.IsSuccess)
                return View(producto.Data);

            return View();
        }

        // POST: ProductosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Usuario objUsuario)
        {
            try
            {
                var resultCreate = _prodDomain.DeleteProducto(id);

                if (resultCreate.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar el producto.");
            }

            return View();
        }
    }
}
