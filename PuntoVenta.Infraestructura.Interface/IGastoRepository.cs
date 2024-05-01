using PuntoVenta.Dominio.Entity;

namespace PuntoVenta.Infraestructura.Interface
{
    public interface IGastoRepository
    {
        ICollection<Gasto> GetGastos();
        ICollection<Gasto> GetGastosByCaja(Guid IdCaja);
        Gasto GetGasto(Guid IdGasto);
        bool CreateGasto(Gasto objGasto);
        bool UpdateGasto(Gasto objGasto);
    }
}