using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PuntoVenta.Dominio.Entity;
using PuntoVenta.Dominio.Interface;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Transversal.Common;
using PuntoVenta.Transversal.Enums;
using PuntoVenta.Transversal.Helpers;
using PuntoVenta.Transversal.Register;

namespace PuntoVenta.Presentacion.Test
{
	public class UsuariosUnitTest
	{
		private ServiceProvider _serviceProvider;
		private IConfiguration _configuration;

		[SetUp]
		public void Setup()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			_configuration = builder.Build();

			var serviceCollection = new ServiceCollection();

			IoCRegister.AddRegistration(serviceCollection);

			serviceCollection.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

			// test
			//serviceCollection.AddDbContext<ApplicationDbContext>(options =>
			//	options.UseInMemoryDatabase("TestDatabase"));

			_serviceProvider = serviceCollection.BuildServiceProvider();
		}

		[Test]
		public void TestCreate()
		{
			var scope = _serviceProvider.CreateScope();
			var service = scope.ServiceProvider.GetRequiredService<IUsuarioDominio>();

            var usuario = new UsuarioAux()
            {
                Nombre = "Nombre",
                UserName = "UserName",
                IdRol = EnumRoles.Administrador,
				Password = "UserName"
            };

            var response = service.CreateUsuario(usuario);

			Assert.IsTrue(response.IsSuccess);
		}

		[Test]
		public void TestRead()
		{
			var scope = _serviceProvider.CreateScope();

			var service = scope.ServiceProvider.GetRequiredService<IUsuarioDominio>();

			var resultado = service.ExistUsuario("test");

			Assert.IsTrue(resultado.IsSuccess);
		}

		[Test]
		public void TestUpdate()
		{
			var scope = _serviceProvider.CreateScope();
			var service = scope.ServiceProvider.GetRequiredService<IUsuarioDominio>();
			var usuario = new Usuario();
			var usuarios = service.GetAllUsuarios();

			if (usuarios.IsSuccess)
			{
				usuario = usuarios.Data.FirstOrDefault();
				usuario.Nombre = "Nuevo Test";
				//var resultUpdate = service.UpdateUsuario(usuario);
				//Assert.IsTrue(resultUpdate.IsSuccess);
			}
		}

		[Test]
		public void TestDelete()
		{
			var scope = _serviceProvider.CreateScope();
			var service = scope.ServiceProvider.GetRequiredService<IUsuarioDominio>();
			var usuario = new Usuario();
			var usuarios = service.GetAllUsuarios();

			if (usuarios.IsSuccess)
			{
				usuario = usuarios.Data.FirstOrDefault();
				var resultUpdate = service.DeleteUsuario(usuario.Id);
				Assert.IsTrue(resultUpdate.IsSuccess);
			}
		}

		[Test]
		public void TestLogin_Success()
		{
			var scope = _serviceProvider.CreateScope();
			var service = scope.ServiceProvider.GetRequiredService<IUsuarioDominio>();

			var response = service.Login("SniperWolf", "SniperWolf");

			Assert.IsTrue(response.IsSuccess);
		}

		[Test]
		public void TestLogin_Failed()
		{
			var scope = _serviceProvider.CreateScope();
			var service = scope.ServiceProvider.GetRequiredService<IUsuarioDominio>();

			var response = service.Login("SniperWolf", "otracosa");

			Assert.IsTrue(response.IsSuccess);
		}

		[TearDown]
		public void Teardown()
		{
			_serviceProvider.Dispose();
		}
	}
}