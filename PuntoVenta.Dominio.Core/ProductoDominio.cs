using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Dominio.Core
{
    public class ProductoDominio : IProductoDominio
    {
        private readonly IProductoRepository _productRepo;
        public ProductoDominio(IProductoRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public GenericResponse<ICollection<Producto>> GetProductos()
        {
            var response = new GenericResponse<ICollection<Producto>>();

            try
            {
                var productos = _productRepo.GetProductos();
                response.Data = productos;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Producto> GetProducto(Guid IdProducto)
        {
            var response = new GenericResponse<Producto>();

            try
            {
                var productos = _productRepo.GetProducto(IdProducto);
                response.Data = productos;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<Producto> GetProductoByCode(string code)
        {
            var response = new GenericResponse<Producto>();

            try
            {
                var productos = _productRepo.GetProductoByCode(code);
                response.Data = productos;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> CreateProducto(Producto ObjProducto)
        {
            var response = new GenericResponse<bool>();

            try
            {
                ObjProducto.FechaCreacion = DateTime.Now;
                ObjProducto.IdEstado = EnumEstados.Activo;
                var productos = _productRepo.CreateProducto(ObjProducto);
                response.Data = productos;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> UpdateProducto(Producto ObjProducto)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var prodOld = _productRepo.GetProducto(ObjProducto.Id);

                ObjProducto.FechaCreacion = prodOld.FechaCreacion;
                ObjProducto.FechaActualizacion = DateTime.Now;

                response.Data = _productRepo.UpdateProducto(ObjProducto);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse<bool> DeleteProducto(Guid IdProducto)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var prodOld = _productRepo.GetProducto(IdProducto);
                prodOld.IdEstado = EnumEstados.Eliminado;
                prodOld.FechaActualizacion = DateTime.Now;

                response.Data = _productRepo.UpdateProducto(prodOld);
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