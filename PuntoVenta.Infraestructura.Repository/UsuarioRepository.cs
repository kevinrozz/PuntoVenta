using Microsoft.EntityFrameworkCore;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Enums;
using PuntoVenta.Transversal.Helpers;

namespace PuntoVenta.Infraestructura.Repository
{
	public class UsuarioRepository : IUsuarioRepository
	{
		private readonly ApplicationDbContext _bd;
		public UsuarioRepository(ApplicationDbContext bd)
		{
			_bd = bd;
		}

		public bool CreateUsuario(Usuario ObjUsuario)
		{
			_bd.Usuario.Add(ObjUsuario);
			return Save();
		}

		public bool ExistUsuario(Guid IdUsuario)
		{
			var resp = _bd.Usuario.AsNoTracking().Any(user => user.Id == IdUsuario);
			return resp;
		}

		public bool ExistUsuario(string Nombre)
		{
			var resp = _bd.Usuario.AsNoTracking().Any(user => user.Nombre == Nombre);
			return resp;
		}

		public IEnumerable<Usuario> GetAllUsuarios()
		{
			var resp = _bd.Usuario.AsNoTracking()
				.ToList();

			return resp;
		}

		public IEnumerable<Usuario> GetAllUsuariosActivos()
		{
			var resp = _bd.Usuario.AsNoTracking()
				.Where(user => user.IdEstado == EnumEstadosUsuario.Activo)
				.ToList();

			return resp;
		}

		public Usuario GetUsuario(Guid IdUsuario)
		{
			var resp = _bd.Usuario.FirstOrDefault(user => user.Id == IdUsuario);
			return resp;
		}

		public Usuario Login(string UserName, string Password)
		{
			var user = _bd.Usuario.AsNoTracking()
				.FirstOrDefault(x => x.UserName == UserName && new List<EnumEstadosUsuario>() { EnumEstadosUsuario.Activo, EnumEstadosUsuario.Operando }.Contains(x.IdEstado));

			if (user == null) return null;

			if (!Utilidades.VerififyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}

		public bool UpdateUsuario(Usuario ObjUsuario)
		{
			var itemTrack = _bd.Usuario.Find(ObjUsuario.Id);

			_bd.Entry(itemTrack).CurrentValues.SetValues(ObjUsuario);

			return Save();
		}

		public bool Save()
		{
			return _bd.SaveChanges() >= 0;
		}
	}
}