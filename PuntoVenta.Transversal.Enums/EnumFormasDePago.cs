using System.ComponentModel;

namespace PuntoVenta.Transversal.Enums
{
	public enum EnumFormasDePago
    {
		[Description("Efectivo")]
        Efectivo = 1,
        [Description("Nequi")]
        Nequi = 2,
        [Description("DaviPlata")]
        DaviPlata = 3,
        [Description("TarjetaDC")]
        TarjetaDC = 4
    }
}