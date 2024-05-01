using Microsoft.Extensions.DependencyInjection;
using PuntoVenta.Dominio.Core;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Interface;
using PuntoVenta.Infraestructura.Repository;

namespace PuntoVenta.Transversal.Register
{
	public static class IoCRegister
	{
		public static IServiceCollection AddRegistration(IServiceCollection services)
		{
			RegisrarDominios(services);
			RegisrarRepositorios(services);

			return services;
		}

		private static void RegisrarDominios(IServiceCollection services)
		{
            services.AddScoped<IUsuarioDominio, UsuarioDominio>();
            services.AddScoped<ICategoriaDominio, CategoriaDominio>();
			services.AddScoped<IProductoDominio, ProductoDominio>();
			services.AddScoped<ICajaDominio, CajaDominio>();
			services.AddScoped<IVentaDominio, VentaDominio>();
			services.AddScoped<IGastoDominio, GastoDominio>();
        }

		private static void RegisrarRepositorios(IServiceCollection services)
		{
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			services.AddScoped<ICategoriaRepository, CategoriaRepository>();
			services.AddScoped<IProductoRepository, ProductoRepository>();
			services.AddScoped<ICajaRepository, CajaRepository>();
			services.AddScoped<IVentaRepository, VentaRepository>();
			services.AddScoped<IGastoRepository, GastoRepository>();
        }
    }
}