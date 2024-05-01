using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Dominio.Entity
{
    public class Venta : Entity
    {
        public Guid IdCaja { get; set; }
        [ForeignKey("IdCaja")]
        public Caja? Caja { get; set; }
        [Display(Name = "Fecha Venta")]
        public DateTime FechaVenta { get; set; }
        [Display(Name = "Estado")]
        public EnumEstadosVenta IdEstado { get; set; }
        [Display(Name = "Forma de Pago")]
        public EnumFormasDePago IdFormaPago { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        [Display(Name = "Monto Recibido")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MontoPago { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Consecutivo { get; set; }
        public List<VentaDetalle>? Detalles { get; set; }
    }
}