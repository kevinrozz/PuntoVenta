using PuntoVenta.Dominio.Entity;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Transversal.Enums;

namespace PuntoVenta.Infraestructura.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _bd;

        public CategoriaRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public bool CreateCategoria(Categoria objCategoria)
        {
            _bd.Categoria.Add(objCategoria);
            return Save();
        }

        public Categoria GetCategoria(Guid Id)
        {
            var categoria = _bd.Categoria.FirstOrDefault(cate => cate.Id == Id);
            return categoria;
        }

        public ICollection<Categoria> GetCategorias()
        {
            var categorias = _bd.Categoria.Where(cate => cate.IdEstado != EnumEstados.Eliminado).ToList();

            return categorias;
        }

        public bool UpdateCategoria(Categoria objCategoria)
        {
            var itemTrack = _bd.Categoria.Find(objCategoria.Id);

            _bd.Entry(itemTrack).CurrentValues.SetValues(objCategoria);

            return Save();
        }

        public bool Save()
        {
            return _bd.SaveChanges() >= 0;
        }
    }
}