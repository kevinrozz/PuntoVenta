using PuntoVenta.Dominio.Entity;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Infraestructura.Interface
{
    public interface ICajaRepository
    {
        ICollection<Caja> GetAllCajas();
        ICollection<Caja> GetCajasByEstado(EnumEstadosCaja status);
        ICollection<Caja> GetCajasByUsuario(Guid IdUsuario);
        Caja GetCaja(Guid IdCaja);
        Caja GetCajaAbiertaByUser(Guid IdUsuario);
        bool CreateCaja(Caja ObjCaja);
        bool UpdateCaja(Caja ObjCaja);
    }
}