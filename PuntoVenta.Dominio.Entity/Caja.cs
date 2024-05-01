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
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal{ get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal VentaTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotalGastos { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorNeto { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Sobrante { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Faltante { get; set; }
        public string? Observaciones { get; set; }
        public EnumEstadosCaja IdEstado { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
    }
}