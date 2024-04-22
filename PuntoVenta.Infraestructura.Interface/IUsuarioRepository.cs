using PuntoVenta.Dominio.Entity;

namespace PuntoVenta.Infraestructura.Interface
{
	public interface IUsuarioRepository
	{
		IEnumerable<Usuario> GetAllUsuarios();
		IEnumerable<Usuario> GetAllUsuariosActivos();
		Usuario GetUsuario(Guid IdUsuario);
		bool ExistUsuario(Guid IdUsuario);
		bool ExistUsuario(string Nombre);
		bool CreateUsuario(Usuario ObjUsuario);
		bool UpdateUsuario(Usuario ObjUsuario);
        Usuario Login(string UserName, string Password);
	}
}