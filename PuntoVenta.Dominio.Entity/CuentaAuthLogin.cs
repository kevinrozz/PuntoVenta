using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Dominio.Entity
{
    public class CuentaAuthLogin
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El Username es obligatorio.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El Password es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Token { get; set; }
        public EnumEstadosUsuario IdEstado { get; set; }
        public EnumRoles IdRol{ get; set; }
    }
}