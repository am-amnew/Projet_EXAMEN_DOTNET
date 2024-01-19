using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
namespace WebApplication2.dal
{

    public class LivreDbContext : DbContext
    {
        public LivreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Abonne> Abonnes { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }
        public DbSet<UserRegister> UserRegisters { get; set; } // Add UserRegister DbSet


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Livre)
                .WithMany(l => l.Emprunts)
                .HasForeignKey(e => e.LivreId);

            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Abonne)
                .WithMany(a => a.Emprunts)
                .HasForeignKey(e => e.AbonneId);

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }


}
