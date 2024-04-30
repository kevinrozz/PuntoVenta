using Microsoft.EntityFrameworkCore;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Infraestructura.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _bd;
        public ProductoRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CreateProducto(Producto ObjProducto)
        {
            _bd.Producto.Add(ObjProducto);
            return Save();
        }

        public Producto GetProducto(Guid IdProducto)
        {
            Producto response = new Producto();

            if (_bd.Producto.Any(pr => pr.Id == IdProducto))
                response = _bd.Producto.Include(c => c.Categoria).FirstOrDefault(pro => pro.Id == IdProducto);

            return response;
        }

        public ICollection<Producto> GetProductos()
        {
            var productos = _bd.Producto.AsTracking()
                .Include(c => c.Categoria)
                .Where(pr => pr.IdEstado == EnumEstados.Activo);

            return productos.ToList();
        }

        public bool UpdateProducto(Producto ObjProducto)
        {
            var itemTrack = _bd.Producto.Find(ObjProducto.Id);

            _bd.Entry(itemTrack).CurrentValues.SetValues(ObjProducto);

            return Save();
        }

        public bool Save()
        {
            return _bd.SaveChanges() >= 0;
        }
    }
}