using Microsoft.EntityFrameworkCore;
namespace TiendaDeDisfraces.Models{
public class TiendaDeDisfracesContext:DbContext{
public TiendaDeDisfracesContext(DbContextOptions<TiendaDeDisfracesContext> o):base(o){}
public DbSet<Usuario> Usuarios { get; set; }
public DbSet<Producto> Productos{get;set;}
}}