using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVenta.Dominio.Entity;

namespace PuntoVentaPresentacion.Web.Models.ViewModels
{
    public class ProductoViewModel
    {
        public IEnumerable<SelectListItem>? Categorias { get; set; }
        public Producto Producto { get; set; }
    }
}