using PuntoVenta.Dominio.Entity;

namespace PuntoVenta.Infraestructura.Interface
{
	public interface IVentaRepository
    {
		IEnumerable<Venta> GetAllVentas();
		IEnumerable<Venta> GetAllVentasByCaja(Guid IdCaja);
		IEnumerable<Venta> GetAllVentasByUsuario(Guid IdIUsuario);
        Venta GetVenta(Guid IdVenta);
		bool CreateVenta(Venta ObjVenta);
		bool UpdateVenta(Venta ObjVenta);
	}
}