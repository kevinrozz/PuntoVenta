using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Dominio.Entity
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}