using System.ComponentModel;

namespace PuntoVenta.Transversal.Enums
{
	public enum EnumEstados
	{
		[Description("Activo")]
		Activo = 1,
		[Description("Inactivo")]
		Inactivo = 2,
		[Description("Deshabilitado")]
		Deshabilitado = 3,
		[Description("Eliminado")]
		Eliminado = 4
	}
}