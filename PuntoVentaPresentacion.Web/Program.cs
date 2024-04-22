using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Infraestructura.Data;
using PuntoVenta.Transversal.Mappers;
using PuntoVenta.Transversal.Register;

var builder = WebApplication.CreateBuilder(args);

// Agregamos Autenticacion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options => {
		options.Cookie.HttpOnly = true;
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		options.LoginPath = "/Home/Login";
		options.AccessDeniedPath = "/Home/AccessDenied";
		options.SlidingExpiration = true;
	});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
{
	Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	Options.EnableSensitiveDataLogging(true);
});

builder.Services.AddAutoMapper(typeof(MappingsProfile));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();

IoCRegister.AddRegistration(builder.Services);

builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromMinutes(60);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Damos soporte para CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();