using Microsoft.EntityFrameworkCore;

namespace TiendaDeDisfraces.Models
{
    /// <summary>
    /// Contexto principal de la base de datos para la aplicación TiendaDeDisfraces.
    /// Administra las entidades y su configuración en Entity Framework Core.
    /// </summary>
    public class TiendaDeDisfracesContext : DbContext
    {
        /// <summary>
        /// Constructor del contexto.
        /// Recibe las opciones de configuración desde Program.cs.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto.</param>
        public TiendaDeDisfracesContext(DbContextOptions<TiendaDeDisfracesContext> options)
            : base(options)
        {
        }

        // =========================
        // DBSETS
        // =========================
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        /// <summary>
        /// Configuración adicional del modelo y restricciones de la base de datos.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de Entity Framework.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);
        }
    }
}
