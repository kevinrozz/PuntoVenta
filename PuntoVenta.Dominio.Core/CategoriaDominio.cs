using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Dominio.Core
{
    public class CategoriaDominio : ICategoriaDominio
    {
        private readonly ICategoriaRepository _repo;
        public CategoriaDominio(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public GenericResponse<bool> CreateCategoria(Categoria objCategoria)
        {
            var response = new GenericResponse<bool>();

            try
            {
                objCategoria.IdEstado = EnumEstados.Activo;
                response.Data = _repo.CreateCategoria(objCategoria);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> DeleteCategoria(Guid Id)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var oldCategoria = _repo.GetCategoria(Id);
                oldCategoria.IdEstado = EnumEstados.Eliminado;

                response.Data = _repo.UpdateCategoria(oldCategoria);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Categoria> GetCategoria(Guid Id)
        {
            var response = new GenericResponse<Categoria>();

            try
            {
                response.Data = _repo.GetCategoria(Id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<ICollection<Categoria>> GetCategorias()
        {
            var response = new GenericResponse<ICollection<Categoria>>();

            try
            {
                response.Data = _repo.GetCategorias();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> UpdateCategoria(Categoria objCategoria)
        {
            var response = new GenericResponse<bool>();

            try
            {
                response.Data = _repo.UpdateCategoria(objCategoria);
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