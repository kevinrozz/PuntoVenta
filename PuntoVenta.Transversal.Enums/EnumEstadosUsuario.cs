using System.ComponentModel;

namespace PuntoVenta.Transversal.Enums
{
	public enum EnumEstadosUsuario
    {
		[Description("Activo")]
		Activo = 1,
		[Description("Inactivo")]
		Inactivo = 2,
		[Description("Deshabilitado")]
		Deshabilitado = 3,
        [Description("Eliminado")]
        Eliminado = 4,
        [Description("Operando")]
        Operando = 5
    }
}