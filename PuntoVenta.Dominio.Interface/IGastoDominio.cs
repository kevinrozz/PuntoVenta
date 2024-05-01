using PuntoVenta.Dominio.Entity;
using PuntoVenta.Transversal.Common;

namespace PuntoVenta.Dominio.Interface
{
    public interface IGastoDominio
    {
        GenericResponse<ICollection<Gasto>> GetGastos();
        GenericResponse<ICollection<Gasto>> GetGastosByCaja(Guid IdCaja);
        GenericResponse<Gasto> GetGasto(Guid IdGasto);
        GenericResponse<bool> CreateGasto(Gasto objGasto);
        GenericResponse<bool> UpdateGasto(Gasto objGasto);
        GenericResponse<bool> DeleteGasto(Guid IdGasto);
    }
}