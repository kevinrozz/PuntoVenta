using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Infraestructura.Data;
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

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();

IoCRegister.AddRegistration(builder.Services);

builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromMinutes(60);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
