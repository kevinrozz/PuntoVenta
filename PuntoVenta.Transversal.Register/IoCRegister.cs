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
        }

		private static void RegisrarRepositorios(IServiceCollection services)
		{
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			services.AddScoped<ICategoriaRepository, CategoriaRepository>();
		}
	}
}