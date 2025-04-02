using Microsoft.EntityFrameworkCore;
using EMGANSA.Models;

namespace EMGANSA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Couleur> Couleurs { get; set; }
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

            modelBuilder.Entity<Voiture>()
                .HasOne(v => v.Couleur)
                .WithMany(c => c.Voitures)
                .HasForeignKey(v => v.CouleurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhotoVoiture>()
                .HasOne(p => p.Voiture)
                .WithMany(v => v.Photos)
                .HasForeignKey(p => p.VoitureId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration des données initiales (seed data)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed des marques
            modelBuilder.Entity<Marque>().HasData(
                new Marque { Id = 1, Nom = "Toyota", Description = "Constructeur automobile japonais" },
                new Marque { Id = 2, Nom = "Renault", Description = "Constructeur automobile français" },
                new Marque { Id = 3, Nom = "Peugeot", Description = "Constructeur automobile français" },
                new Marque { Id = 4, Nom = "Mercedes", Description = "Constructeur automobile allemand" }
            );

            // Seed des modèles
            modelBuilder.Entity<Modele>().HasData(
                new Modele { Id = 1, Nom = "Corolla", MarqueId = 1, Description = "Berline compacte" },
                new Modele { Id = 2, Nom = "Clio", MarqueId = 2, Description = "Citadine polyvalente" },
                new Modele { Id = 3, Nom = "208", MarqueId = 3, Description = "Citadine" },
                new Modele { Id = 4, Nom = "Classe C", MarqueId = 4, Description = "Berline de luxe" }
            );

            // Seed des couleurs
            modelBuilder.Entity<Couleur>().HasData(
                new Couleur { Id = 1, Nom = "Blanc", CodeHex = "#FFFFFF" },
                new Couleur { Id = 2, Nom = "Noir", CodeHex = "#000000" },
                new Couleur { Id = 3, Nom = "Rouge", CodeHex = "#FF0000" },
                new Couleur { Id = 4, Nom = "Bleu", CodeHex = "#0000FF" }
            );
        }
    }
}