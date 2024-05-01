using System.ComponentModel;

namespace PuntoVenta.Transversal.Enums
{
	public enum EnumEstadosVenta
    {
		[Description("Procesada")]
        Procesada = 1,
		[Description("Devuelta")]
        Devuelta = 2
	}
}