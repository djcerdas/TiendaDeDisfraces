using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TiendaDeDisfraces.Models;

var builder = WebApplication.CreateBuilder(args);

#region SERVICIOS

/// <summary>
/// Configuración del contexto de base de datos.
/// </summary>
builder.Services.AddDbContext<TiendaDeDisfracesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

/// <summary>
/// Registro de servicios MVC.
/// </summary>
builder.Services.AddControllersWithViews();

/// <summary>
/// Habilita el uso de sesión en la aplicación.
/// </summary>
builder.Services.AddSession();

#endregion

var app = builder.Build();

#region MIDDLEWARE

/// <summary>
/// Manejo de errores en ambientes no de desarrollo.
/// </summary>
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

/// <summary>
/// Habilita archivos estáticos.
/// </summary>
app.UseStaticFiles();

/// <summary>
/// Habilita el enrutamiento.
/// </summary>
app.UseRouting();

/// <summary>
/// Habilita la sesión.
/// </summary>
app.UseSession();

#endregion

#region RUTAS

/// <summary>
/// Ruta principal del sistema.
/// </summary>
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

#endregion

#region DEBUG BASE DE DATOS

/// <summary>
/// SOLO PARA PRUEBAS:
/// Elimina y recrea la base de datos en cada ejecución.
/// Esto permite que el Setup siempre se ejecute.
/// </summary>
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TiendaDeDisfracesContext>();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

#endregion

app.Run();