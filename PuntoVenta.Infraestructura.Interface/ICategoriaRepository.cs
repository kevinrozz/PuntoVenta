using PuntoVenta.Dominio.Entity;

namespace PuntoVenta.Infraestructura.Interface
{
    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(Guid Id);
        bool CreateCategoria(Categoria objCategoria);
        bool UpdateCategoria(Categoria objCategoria);
    }
}