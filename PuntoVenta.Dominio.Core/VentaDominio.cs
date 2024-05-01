using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Dominio.Core
{
    public class VentaDominio : IVentaDominio
    {
        private readonly IVentaRepository _repo;
        private readonly IProductoRepository _prodRepo;
        public VentaDominio(IVentaRepository repo, IProductoRepository prodRepo)
        {
            _repo = repo;
            _prodRepo = prodRepo;
        }

        public GenericResponse<bool> CreateVenta(Venta ObjVenta)
        {
            var response = new GenericResponse<bool>();

            try
            {
                ObjVenta.FechaVenta = DateTime.Now;
                ObjVenta.IdFormaPago = EnumFormasDePago.Efectivo;
                ObjVenta.IdEstado = EnumEstadosVenta.Procesada;

                foreach (var detalleVenta in ObjVenta.Detalles)
                {
                    var tempProducto = _prodRepo.GetProducto(detalleVenta.IdProducto);

                    if (tempProducto != null)
                    {
                        tempProducto.CantidadStock -= detalleVenta.Cantidad;
                        detalleVenta.Total = tempProducto.PrecioPublico * detalleVenta.Cantidad;
                    }
                }

                ObjVenta.Total = ObjVenta.Detalles.Sum(t => t.Total);
                ObjVenta.SubTotal = ObjVenta.Total;

                response.Data = _repo.CreateVenta(ObjVenta);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<IEnumerable<Venta>> GetAllVentas()
        {
            var response = new GenericResponse<IEnumerable<Venta>>();

            try
            {
                response.Data = _repo.GetAllVentas();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<IEnumerable<Venta>> GetAllVentasByCaja(Guid IdCaja)
        {
            var response = new GenericResponse<IEnumerable<Venta>>();

            try
            {
                response.Data = _repo.GetAllVentasByCaja(IdCaja);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<IEnumerable<Venta>> GetAllVentasByUsuario(Guid IdUsuario)
        {
            var response = new GenericResponse<IEnumerable<Venta>>();

            try
            {
                response.Data = _repo.GetAllVentasByUsuario(IdUsuario);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Venta> GetVenta(Guid IdVenta)
        {
            var response = new GenericResponse<Venta>();

            try
            {
                response.Data = _repo.GetVenta(IdVenta);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> UpdateVenta(Venta ObjVenta)
        {
            var response = new GenericResponse<bool>();

            try
            {
                response.Data = _repo.UpdateVenta(ObjVenta);
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