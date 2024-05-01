using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Dominio.Entity
{
    public class VentaDetalle : Entity
    {
        public Guid IdVenta { get; set; }
        [ForeignKey("IdVenta")]
        public Venta? Venta { get; set; }
        public Guid IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
    }
}