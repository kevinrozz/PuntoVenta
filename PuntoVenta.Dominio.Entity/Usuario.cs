using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Dominio.Entity
{
	public class Usuario : Entity
	{
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        [Display(Name = "Rol")]
        public EnumRoles IdRol { get; set; }
        [Display(Name = "Estado")]
        public EnumEstadosUsuario IdEstado { get; set; }
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Fecha de Actualización")]
        public DateTime? FechaActualizacion { get; set; }
    }
}