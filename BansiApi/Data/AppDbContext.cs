using BansiModelos;
using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Examen> Examenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Examen>().HasData(
                new Examen { IdExamen = 1, Nombre = "Mi Examen", Descripcion = "Examen Christian Sandoval" }
            );
        }
    }
