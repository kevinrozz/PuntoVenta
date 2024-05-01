using Microsoft.EntityFrameworkCore;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Infraestructura.Interface;

namespace PuntoVenta.Infraestructura.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private readonly ApplicationDbContext _bd;
        public VentaRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CreateVenta(Venta ObjVenta)
        {
            _bd.Venta.Add(ObjVenta);
            return Save();
        }

        public IEnumerable<Venta> GetAllVentas()
        {
            var items = _bd.Venta.AsNoTracking().ToList();
            return items;
        }

        public IEnumerable<Venta> GetAllVentasByCaja(Guid IdCaja)
        {
            var items = _bd.Venta.Include(v => v.Detalles).AsNoTracking().Where(t => t.IdCaja == IdCaja).ToList();
            return items;
        }

        public IEnumerable<Venta> GetAllVentasByUsuario(Guid IdUser)
        {
            var items = _bd.Venta.AsNoTracking().Where(t => t.Caja.IdUsuario == IdUser).ToList();
            return items;
        }

        public Venta GetVenta(Guid IdVenta)
        {
            var item = _bd.Venta
                .Include(v => v.Detalles)
                .Select(v => new Venta()
                {
                    Id = v.Id,
                    FechaVenta = v.FechaVenta,
                    IdFormaPago= v.IdFormaPago,
                    SubTotal = v.SubTotal,
                    Total = v.Total,
                    MontoPago = v.MontoPago,
                    Consecutivo = v.Consecutivo,
                    Detalles = v.Detalles.Select(d => new VentaDetalle
                    {
                        Producto = d.Producto,
                        Cantidad = d.Cantidad,
                        Total = d.Total
                    }).ToList()
                })
                .FirstOrDefault(t => t.Id == IdVenta);

            return item;
        }

        public bool UpdateVenta(Venta ObjVenta)
        {
            var itemTrack = _bd.Venta.Find(ObjVenta.Id);
            _bd.Entry(itemTrack).CurrentValues.SetValues(ObjVenta);

            return Save();
        }

        public bool Save()
        {
            return _bd.SaveChanges() >= 0;
        }
    }
}