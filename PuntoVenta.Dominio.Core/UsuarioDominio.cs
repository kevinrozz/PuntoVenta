using AutoMapper;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;
using PuntoVenta.Transversal.Helpers;

namespace PuntoVenta.Dominio.Core
{
	public class UsuarioDominio : IUsuarioDominio
	{
		private readonly IUsuarioRepository _userRepo;
		private readonly IMapper _mapper;

		public UsuarioDominio(IUsuarioRepository userRepo, IMapper mapper)
		{
			_userRepo = userRepo;
			_mapper = mapper;
		}

		public GenericResponse<bool> CreateUsuario(UsuarioAux prmUserAux)
		{
			var response = new GenericResponse<bool>();

			try
			{
                var tupleHashes = Utilidades.CreatePasswordHash(prmUserAux.Password);

                var usuario = new Usuario()
				{
					Nombre = prmUserAux.Nombre,
					UserName = prmUserAux.UserName,
					IdRol = prmUserAux.IdRol,
					IdEstado = EnumEstados.Activo,
					FechaCreacion = DateTime.Now,
					PasswordHash = tupleHashes.Item1,
                    PasswordSalt = tupleHashes.Item2
                };

                var resultCreate = _userRepo.CreateUsuario(usuario);

				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}
			
			return response;
		}

		public GenericResponse<bool> DeleteUsuario(Guid IdUsuario)
		{
			var response = new GenericResponse<bool>();

			try
			{
				var oldUser = _userRepo.GetUsuario(IdUsuario);
				oldUser.FechaActualizacion = DateTime.Now;
				oldUser.IdEstado = EnumEstados.Eliminado;

				response.Data = _userRepo.UpdateUsuario(oldUser);
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public GenericResponse<bool> ExistUsuario(Guid IdUsuario)
		{
			var response = new GenericResponse<bool>();

			try
			{
				response.Data = _userRepo.ExistUsuario(IdUsuario);
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public GenericResponse<bool> ExistUsuario(string Nombre)
		{
			var response = new GenericResponse<bool>();

			try
			{
				response.Data = _userRepo.ExistUsuario(Nombre);
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public GenericResponse<IEnumerable<Usuario>> GetAllUsuarios()
		{
			var response = new GenericResponse<IEnumerable<Usuario>>();

			try
			{
				response.Data = _userRepo.GetAllUsuarios();
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public GenericResponse<IEnumerable<Usuario>> GetAllUsuariosActivos()
		{
			var response = new GenericResponse<IEnumerable<Usuario>>();

			try
			{
				response.Data = _userRepo.GetAllUsuariosActivos();
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public GenericResponse<Usuario> GetUsuario(Guid IdUsuario)
		{
			var response = new GenericResponse<Usuario>();

			try
			{
				response.Data = _userRepo.GetUsuario(IdUsuario);
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public GenericResponse<CuentaAuthLogin> Login(string UserName, string Password)
		{
			var response = new GenericResponse<CuentaAuthLogin>();

			try
			{
				var resultUser = _userRepo.Login(UserName, Password);

				if (resultUser != null)
					response.Data = new CuentaAuthLogin() { Id = resultUser.Id, UserName = resultUser.UserName };
				else
					throw new Exception("Error iniciando Sesion");
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public GenericResponse<bool> UpdateUsuario(Usuario ObjUsuario)
		{
			var response = new GenericResponse<bool>();

			try
			{
				var userOld = _userRepo.GetUsuario(ObjUsuario.Id);
                userOld.FechaActualizacion = DateTime.Now;
				userOld.Nombre = ObjUsuario.Nombre;
				userOld.UserName = ObjUsuario.UserName;
				userOld.IdRol = ObjUsuario.IdRol;
				userOld.IdEstado = ObjUsuario.IdEstado;

                response.Data = _userRepo.UpdateUsuario(userOld);
				response.IsSuccess = true;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}

			return response;
		}
	}
}