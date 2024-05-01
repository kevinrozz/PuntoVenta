using PuntoVenta.Dominio.Entity;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Dominio.Interface
{
    public interface ICajaDominio
    {
        GenericResponse<ICollection<Caja>> GetAllCajas();
        GenericResponse<ICollection<Caja>> GetCajasByEstado(EnumEstadosCaja status);
        GenericResponse<ICollection<Caja>> GetCajasByUsuario(Guid IdUsuario);
        GenericResponse<Caja> GetCaja(Guid IdCaja);
        GenericResponse<Caja> GetTotales(Guid IdCaja);
        GenericResponse<Caja> GetCajaAbiertaByUser(Guid IdUsuario);
        GenericResponse<bool> AbrirCaja(Caja ObjCaja);
        GenericResponse<bool> CerrarCaja(Caja ObjCaja);
    }
}