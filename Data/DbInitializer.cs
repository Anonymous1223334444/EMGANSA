using System;
using System.Linq;
using EMGANSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EMGANSA.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Assurez-vous que la base de données est créée
                context.Database.EnsureCreated();

                // Vérifiez si des voitures existent déjà pour éviter de recréer des données
                if (context.Voitures.Any())
                {
                    return; // La base de données a déjà été initialisée
                }

                // Ajout de quelques voitures d'exemple
                var voitures = new Voiture[]
                {
                    new Voiture {
                        ModeleId = 1,
                        CouleurId = 1,
                        Annee = 2020,
                        Kilometrage = 25000,
                        Prix = 18500M,
                        DateAcquisition = new DateTime(2025, 04, 02),
                        Description = "Toyota Corolla en excellent état",
                        Statut = StatutVoiture.Disponible
                    },
                    new Voiture {
                        ModeleId = 2,
                        CouleurId = 3,
                        Annee = 2019,
                        Kilometrage = 35000,
                        Prix = 12900M,
                        DateAcquisition = new DateTime(2025, 04, 02),
                        Description = "Renault Clio avec faible kilométrage",
                        Statut = StatutVoiture.Disponible
                    }
                };

                context.Voitures.AddRange(voitures);
                context.SaveChanges();

                // Ajout des photos pour les voitures
                var photos = new PhotoVoiture[]
                {
                    new PhotoVoiture {
                        VoitureId = 1,
                        CheminImage = "/images/voitures/corolla_1.jpg",
                        Titre = "Toyota Corolla - Vue profile",
                        EstPrincipale = true
                    },
                    new PhotoVoiture {
                        VoitureId = 2,
                        CheminImage = "/images/voitures/clio_1.jpg",
                        Titre = "Renault Clio - Vue avant",
                        EstPrincipale = true
                    }
                };

                context.PhotosVoitures.AddRange(photos);
                context.SaveChanges();
            }
        }
    }
}