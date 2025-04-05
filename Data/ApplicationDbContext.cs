using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EMGANSA.Models;

namespace EMGANSA.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<PhotoVoiture> PhotosVoitures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations
            modelBuilder.Entity<Modele>()
                .HasOne(m => m.Marque)
                .WithMany(m => m.Modeles)
                .HasForeignKey(m => m.MarqueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voiture>()
                .HasOne(v => v.Modele)
                .WithMany(m => m.Voitures)
                .HasForeignKey(v => v.ModeleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhotoVoiture>()
                .HasOne(p => p.Voiture)
                .WithMany(v => v.Photos)
                .HasForeignKey(p => p.VoitureId)
                .OnDelete(DeleteBehavior.Cascade);

            // Données initiales (seed data)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed des marques
            modelBuilder.Entity<Marque>().HasData(
                new Marque { Id = 1, Nom = "Toyota", Description = "Constructeur automobile japonais" },
                new Marque { Id = 2, Nom = "Renault", Description = "Constructeur automobile français" },
                new Marque { Id = 3, Nom = "Peugeot", Description = "Constructeur automobile français" }
            );

            // Seed des modèles
            modelBuilder.Entity<Modele>().HasData(
                new Modele { Id = 1, Nom = "Corolla", MarqueId = 1 },
                new Modele { Id = 2, Nom = "Clio", MarqueId = 2 },
                new Modele { Id = 3, Nom = "208", MarqueId = 3 }
            );
        }
    }
}