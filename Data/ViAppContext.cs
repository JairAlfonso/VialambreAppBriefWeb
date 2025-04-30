using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;

namespace VialambreAppTest1.Data
{
    public class ViAppContext : IdentityDbContext
    {
        public ViAppContext(DbContextOptions<ViAppContext> options) : base(options)
        {
        }

        protected ViAppContext()
        {
        }

        public DbSet<AppUser> AppUser { get; set; }

        public DbSet<Brief> Brief { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Empaque> Empaque { get; set; }

        public DbSet<LugarEntrega> LugarEntrega { get; set; }


        public DbSet<Pieza> Pieza { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Configuración del campo ValorTotalCotizado en la entidad Brief
            //modelBuilder.Entity<Brief>()
            //    .Property(b => b.ValorTotalCotizado)
            //    .HasColumnType("decimal(18,0)")  // Define el tipo de columna en la BD
            //    .HasDefaultValue(0)  // Asegura que los valores antiguos sean 0 en lugar de NULL
            //    .IsRequired();  // No permite valores nulos
            
            modelBuilder.Entity<Brief>()
        .Property(b => b.ValorTotalCotizado)
        .HasPrecision(18, 2); // 18 dígitos totales, 2 decimales
        }
    }
}