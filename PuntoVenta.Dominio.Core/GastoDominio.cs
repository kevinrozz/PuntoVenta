using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Dominio.Core
{
    public class GastoDominio : IGastoDominio
    {
        private readonly IGastoRepository _gastoRepo;
        public GastoDominio(IGastoRepository gastoRepo)
        {
            _gastoRepo = gastoRepo;
        }

        public GenericResponse<bool> CreateGasto(Gasto objGasto)
        {
            var response = new GenericResponse<bool>();

            try
            {
                objGasto.Fecha= DateTime.Now;
                objGasto.IdEstado = EnumEstados.Activo;
                var result = _gastoRepo.CreateGasto(objGasto);

                response.Data = result;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> DeleteGasto(Guid IdGasto)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var objGasto = _gastoRepo.GetGasto(IdGasto);
                objGasto.IdEstado = EnumEstados.Eliminado;

                var result = _gastoRepo.UpdateGasto(objGasto);
                response.Data = result;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Gasto> GetGasto(Guid IdGasto)
        {
            var response = new GenericResponse<Gasto>();

            try
            {
                response.Data = _gastoRepo.GetGasto(IdGasto);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<ICollection<Gasto>> GetGastos()
        {
            var response = new GenericResponse<ICollection<Gasto>>();

            try
            {
                response.Data = _gastoRepo.GetGastos();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<ICollection<Gasto>> GetGastosByCaja(Guid IdCaja)
        {
            var response = new GenericResponse<ICollection<Gasto>>();

            try
            {
                response.Data = _gastoRepo.GetGastosByCaja(IdCaja);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> UpdateGasto(Gasto objGasto)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var result = _gastoRepo.UpdateGasto(objGasto);
                response.Data = result;
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