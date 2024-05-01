using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Dominio.Entity
{
    public class Caja : Entity
    {
        public Guid IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Valor Inicial")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public decimal ValorInicial { get; set; }
        [Display(Name = "Valor Total")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal{ get; set; }
        [Display(Name = "Venta Total")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal VentaTotal { get; set; }
        [Display(Name = "Valor Total Gastos")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotalGastos { get; set; }
        [Display(Name = "Valor Neto")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorNeto { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Sobrante { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Faltante { get; set; }
        public string? Observaciones { get; set; }
        [Display(Name = "Estado")]
        public EnumEstadosCaja IdEstado { get; set; }
        [Display(Name = "Fecha Apertura")]
        public DateTime FechaApertura { get; set; }
        [Display(Name = "Fecha Cierre")]
        public DateTime? FechaCierre { get; set; }
    }
}