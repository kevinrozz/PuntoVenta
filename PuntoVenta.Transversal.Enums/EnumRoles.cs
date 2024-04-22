using System.ComponentModel;

namespace PuntoVenta.Transversal.Enums
{
	public enum EnumRoles
	{
		[Description("SuperAdministrador")]
		SuperAdministrador = 0,
		[Description("Administrador")]
		Administrador = 1,
		[Description("Cajero")]
		Cajero = 2
	}
}