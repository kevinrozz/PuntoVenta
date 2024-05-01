using PuntoVenta.Dominio.Entity;
using PuntoVenta.Transversal.Common;

namespace PuntoVenta.Dominio.Interface
{
    public interface IVentaDominio
    {
        GenericResponse<IEnumerable<Venta>> GetAllVentas();
        GenericResponse<IEnumerable<Venta>> GetAllVentasByCaja(Guid IdCaja);
        GenericResponse<IEnumerable<Venta>> GetAllVentasByUsuario(Guid IdUser);
        GenericResponse<Venta> GetVenta(Guid IdVenta);
        GenericResponse<bool> CreateVenta(Venta ObjVenta);
        GenericResponse<bool> UpdateVenta(Venta ObjVenta);
    }
}