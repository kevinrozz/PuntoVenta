using Microsoft.EntityFrameworkCore;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Infraestructura.Repository
{
    public class GastoRepository : IGastoRepository
    {
        private readonly ApplicationDbContext _bd;
        public GastoRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CreateGasto(Gasto objGasto)
        {
            _bd.Gasto.Add(objGasto);
            return Save();
        }

        public Gasto GetGasto(Guid IdGasto)
        {
            var item = _bd.Gasto.FirstOrDefault(t => t.Id == IdGasto);
            return item;
        }

        public ICollection<Gasto> GetGastos()
        {
            var items = _bd.Gasto.AsNoTracking().ToList();
            return items;
        }

        public ICollection<Gasto> GetGastosByCaja(Guid IdCaja)
        {
            var items = _bd.Gasto.AsNoTracking()
                .Where(t => t.IdCaja == IdCaja && t.IdEstado == EnumEstados.Activo)
                .ToList();

            return items;
        }

        public bool UpdateGasto(Gasto objGasto)
        {
            var itemTrack = _bd.Gasto.Find(objGasto.Id);

            _bd.Entry(itemTrack).CurrentValues.SetValues(objGasto);

            return Save();
        }

        public bool Save()
        {
            return _bd.SaveChanges() >= 0;
        }
    }
}