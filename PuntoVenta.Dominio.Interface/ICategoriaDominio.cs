using PuntoVenta.Dominio.Entity;
using PuntoVenta.Transversal.Common;

namespace PuntoVenta.Dominio.Interface
{
    public interface ICategoriaDominio
    {
        GenericResponse<ICollection<Categoria>> GetCategorias();
        GenericResponse<Categoria> GetCategoria(Guid Id);
        GenericResponse<bool> CreateCategoria(Categoria objCategoria);
        GenericResponse<bool> UpdateCategoria(Categoria objCategoria);
        GenericResponse<bool> DeleteCategoria(Guid Id);
    }
}