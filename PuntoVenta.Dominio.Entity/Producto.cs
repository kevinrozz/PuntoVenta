using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Dominio.Entity
{
    public class Producto : Entity
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public Guid IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Costo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioPublico { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int CantidadStock { get; set; }
        [Display(Name = "Estado")]
        public EnumEstados IdEstado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

    }
}