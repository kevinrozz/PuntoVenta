using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Transversal.Enums;
using PuntoVentaPresentacion.Web.Models;

namespace PuntoVentaPresentacion.Web.Controllers
{
	public class UsuariosController : Controller
	{
		private readonly IUsuarioDominio _userDomain;
        public UsuariosController(IUsuarioDominio userDomain)
        {
			_userDomain = userDomain;
		}

        // GET: UsuariosController
        public ActionResult Index()
		{
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Home");

            var usuarios = _userDomain.GetAllUsuarios();
			
			if (usuarios.IsSuccess)
				return View(usuarios.Data);

            return View();
        }

		// GET: UsuariosController/Details/5
		public ActionResult Details(Guid id)
		{
			var usuario = _userDomain.GetUsuario(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View(usuario);
		}

		// GET: UsuariosController/Create
		public ActionResult Create()
		{
            ViewData["ListRoles"] = new SelectList(Enum.GetNames(typeof(EnumRoles)));
            var objuser = new UsuarioAux();

			return View(objuser);
		}

        /// POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioAux usuarioModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _userDomain.CreateUsuario(usuarioModel);

					if(resultCreate.IsSuccess)
						return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el usuario.");
                }
            }
            ViewData["ListRoles"] = new SelectList(Enum.GetNames(typeof(EnumRoles)));
            return View(usuarioModel);
        }


        // GET: UsuariosController/Edit/5
        public ActionResult Edit(Guid id)
		{
            ViewData["ListRoles"] = new SelectList(Enum.GetNames(typeof(EnumRoles)));
            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            var usuario = _userDomain.GetUsuario(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View();
		}

		// POST: UsuariosController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid id, Usuario usuarioModel)
		{
            if (ModelState.IsValid)
            {
                try
                {
                    var resultCreate = _userDomain.UpdateUsuario(usuarioModel);

                    if (resultCreate.IsSuccess)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el usuario.");
                }
            }

            ViewData["ListRoles"] = new SelectList(Enum.GetNames(typeof(EnumRoles)));
            ViewData["ListEstados"] = new SelectList(Enum.GetNames(typeof(EnumEstados)));

            return View(usuarioModel);
        }

		// GET: UsuariosController/Delete/5
		public ActionResult Delete(Guid id)
		{
			var usuario = _userDomain.GetUsuario(id);

            if (usuario.IsSuccess)
                return View(usuario.Data);

            return View();
		}

		// POST: UsuariosController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, Usuario objUsuario)
		{
            try
            {
                var resultCreate = _userDomain.DeleteUsuario(id);

                if (resultCreate.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar el usuario.");
            }

            return View();
        }
	}
}
