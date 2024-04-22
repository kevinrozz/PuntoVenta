using PuntoVenta.Dominio.Entity;
using PuntoVenta.Transversal.Common;

namespace PuntoVenta.Dominio.Interface
{
	public interface IUsuarioDominio
	{
		GenericResponse<IEnumerable<Usuario>> GetAllUsuarios();
		GenericResponse<IEnumerable<Usuario>> GetAllUsuariosActivos();
		GenericResponse<Usuario> GetUsuario(Guid IdUsuario);
		GenericResponse<bool> ExistUsuario(Guid IdUsuario);
		GenericResponse<bool> ExistUsuario(string Nombre);
		GenericResponse<bool> CreateUsuario(UsuarioAux ObjUsuario);
		GenericResponse<bool> UpdateUsuario(Usuario ObjUsuario);
		GenericResponse<CuentaAuthLogin> Login(string UserName, string Password);
		GenericResponse<bool> DeleteUsuario(Guid IdUsuario);
	}
}