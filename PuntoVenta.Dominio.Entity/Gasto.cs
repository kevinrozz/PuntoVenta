using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Dominio.Entity
{
    public class Gasto : Entity
    {
        public Guid IdCaja { get; set; }
        [ForeignKey("IdCaja")]
        public Caja? Caja { get; set; }
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string? Concepto { get; set; }
        [Display(Name = "Estado")]
        public EnumEstados IdEstado { get; set; }
    }
}