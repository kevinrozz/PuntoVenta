using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Dominio.Core
{
    public class CajaDominio : ICajaDominio
    {
        private readonly ICajaRepository _cajaRepo;
        private readonly IVentaRepository _ventaRepo;
        private readonly IUsuarioRepository _userRepo;
        private readonly IGastoRepository _gastoRepo;
        public CajaDominio(ICajaRepository cajaRepo, IVentaRepository ventaRepo, IUsuarioRepository userRepo, IGastoRepository gastoRepo)
        {
            _cajaRepo = cajaRepo;
            _ventaRepo = ventaRepo;
            _userRepo = userRepo;
            _gastoRepo = gastoRepo;
        }

        public GenericResponse<bool> AbrirCaja(Caja ObjCaja)
        {
            var response = new GenericResponse<bool>();

            try
            {
                ObjCaja.IdEstado = EnumEstadosCaja.Abierta;
                ObjCaja.FechaApertura = DateTime.Now;
                response.Data = _cajaRepo.CreateCaja(ObjCaja);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<ICollection<Caja>> GetAllCajas()
        {
            var response = new GenericResponse<ICollection<Caja>>();

            try
            {
                response.Data = _cajaRepo.GetAllCajas();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Caja> GetCaja(Guid IdCaja)
        {
            var response = new GenericResponse<Caja>();

            try
            {
                response.Data = _cajaRepo.GetCaja(IdCaja);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Caja> GetTotales(Guid IdCaja)
        {
            var response = new GenericResponse<Caja>();

            try
            {
                var ventasByCaja = _ventaRepo.GetAllVentasByCaja(IdCaja);
                var objCaja = _cajaRepo.GetCaja(IdCaja);

                objCaja.VentaTotal = ventasByCaja.Sum(t => t.Total);

                var gastosCaja = _gastoRepo.GetGastosByCaja(IdCaja);
                objCaja.ValorTotalGastos = gastosCaja.Sum(g => g.Valor);

                response.Data = objCaja;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Caja> GetCajaAbiertaByUser(Guid IdUsuario)
        {
            var response = new GenericResponse<Caja>();

            try
            {
                response.Data = _cajaRepo.GetCajaAbiertaByUser(IdUsuario);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<ICollection<Caja>> GetCajasByEstado(EnumEstadosCaja status)
        {
            var response = new GenericResponse<ICollection<Caja>>();

            try
            {
                response.Data = _cajaRepo.GetCajasByEstado(status);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<ICollection<Caja>> GetCajasByUsuario(Guid IdUsuario)
        {
            var response = new GenericResponse<ICollection<Caja>>();

            try
            {
                response.Data = _cajaRepo.GetCajasByUsuario(IdUsuario);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> CerrarCaja(Caja ObjCaja)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var userCaja = _userRepo.GetUsuario(ObjCaja.IdUsuario);
                userCaja.IdEstado = EnumEstadosUsuario.Activo;
                ObjCaja.IdEstado = EnumEstadosCaja.Cerrada;
                ObjCaja.FechaCierre = DateTime.Now;
                ObjCaja.ValorNeto = ObjCaja.VentaTotal - ObjCaja.ValorTotalGastos;
                var diff = ObjCaja.ValorNeto - ObjCaja.ValorTotal;
                ObjCaja.Faltante = diff > 0 ? diff : 0;
                ObjCaja.Sobrante = diff < 0 ? Math.Abs(diff) : 0;
                response.Data = _cajaRepo.UpdateCaja(ObjCaja);
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