using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Dominio.Entity
{
    public class Categoria : Entity
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Display(Name = "Estado")]
        public EnumEstados IdEstado { get; set; }
    }
}