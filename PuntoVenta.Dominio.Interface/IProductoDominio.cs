using PuntoVenta.Dominio.Entity;
using PuntoVenta.Transversal.Common;

namespace PuntoVenta.Dominio.Interface
{
    public interface IProductoDominio
    {
        GenericResponse<ICollection<Producto>> GetProductos();
        GenericResponse<Producto> GetProducto(Guid IdProducto);
        GenericResponse<bool> CreateProducto(Producto ObjProducto);
        GenericResponse<bool> UpdateProducto(Producto ObjProducto);
        GenericResponse<bool> DeleteProducto(Guid IdProducto);
    }
}