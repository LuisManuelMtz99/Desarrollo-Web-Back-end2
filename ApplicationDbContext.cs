using JuegosApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace JuegosApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JuegoDato>()
                .HasKey(al => new { al.JuegoId, al.DatoId });
        }

        public DbSet<Juego> Juegos { get; set; }
        public DbSet<Dato> Datos { get; set; }

        public DbSet<JuegoDato> JuegoDato { get; set; }
    }
}
