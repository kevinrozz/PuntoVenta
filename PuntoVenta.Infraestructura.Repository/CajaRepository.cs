using Microsoft.EntityFrameworkCore;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Infraestructura.Repository
{
    public class CajaRepository : ICajaRepository
    {
        private readonly ApplicationDbContext _bd;

        public CajaRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CreateCaja(Caja ObjCaja)
        {
            _bd.Caja.Add(ObjCaja);
            return Save();
        }

        public ICollection<Caja> GetAllCajas()
        {
            var items = _bd.Caja.AsNoTracking().ToList();
            return items;
        }

        public Caja GetCaja(Guid IdCaja)
        {
            var item = _bd.Caja.FirstOrDefault(t => t.Id == IdCaja);
            return item;
        }

        public Caja GetCajaAbiertaByUser(Guid IdUsuario)
        {
            var item = _bd.Caja.FirstOrDefault(t => t.IdUsuario == IdUsuario && t.IdEstado == EnumEstadosCaja.Abierta);
            return item;
        }

        public ICollection<Caja> GetCajasByEstado(EnumEstadosCaja status)
        {
            var items = _bd.Caja.AsNoTracking()
                .Where(t => t.IdEstado == status)
                .ToList();

            return items;
        }

        public ICollection<Caja> GetCajasByUsuario(Guid IdUsuario)
        {
            var items = _bd.Caja.AsNoTracking()
                .Where(t => t.IdUsuario == IdUsuario)
                .ToList();

            return items;
        }

        public bool UpdateCaja(Caja ObjCaja)
        {
            var itemTrack = _bd.Caja.Find(ObjCaja.Id);

            _bd.Entry(itemTrack).CurrentValues.SetValues(ObjCaja);

            return Save();
        }

        public bool Save()
        {
            return _bd.SaveChanges() >= 0;
        }
    }
}