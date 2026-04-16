using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TiendaDeDisfraces.Models;

var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<TiendaDeDisfracesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// MVC
builder.Services.AddControllersWithViews();
// Sesión
builder.Services.AddSession();
// NECESARIO PARA USAR HttpContext EN VISTAS
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Manejo de errores (pro)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();