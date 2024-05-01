using PuntoVenta.Dominio.Entity;

namespace PuntoVenta.Infraestructura.Interface
{
	public interface IProductoRepository
    {
        ICollection<Producto> GetProductos();
        Producto GetProducto(Guid IdProducto);
        Producto GetProductoByCode(string code);
        bool CreateProducto(Producto ObjProducto);
        bool UpdateProducto(Producto ObjProducto);
    }
}