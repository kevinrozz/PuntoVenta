using PuntoVenta.Transversal.Enums;
using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Dominio.Entity
{
    public class UsuarioAux
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Confirmacion de Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de contraseña no coinciden.")]
        public string PasswordVerificacion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Rol")]
        public EnumRoles IdRol { get; set; }
        [Display(Name = "Estado")]
        public EnumEstados IdEstado { get; set; }
    }
}