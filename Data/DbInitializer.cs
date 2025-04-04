using EMGANSA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EMGANSA.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // S'assurer que la base de données est créée
            context.Database.EnsureCreated();

            // Vérifier s'il y a déjà des voitures
            if (context.Voitures.Any())
            {
                return; // La base de données a déjà été initialisée
            }

            // Ajouter des voitures de test
            var voitures = new Voiture[]
            {
                new Voiture
                {
                    ModeleId = 1, // Toyota Corolla
                    Annee = 2022,
                    Kilometrage = 15000,
                    Prix = 23500M,
                    DateAcquisition = DateTime.Now.AddMonths(-3),
                    Description = "Toyota Corolla en excellent état, première main, entretien régulier.",
                    Statut = StatutVoiture.Disponible
                },
                new Voiture
                {
                    ModeleId = 2, // Renault Clio
                    Annee = 2020,
                    Kilometrage = 35000,
                    Prix = 14500M,
                    DateAcquisition = DateTime.Now.AddMonths(-1),
                    Description = "Renault Clio bien entretenue, carnet d'entretien à jour, faible consommation.",
                    Statut = StatutVoiture.Disponible
                },
                new Voiture
                {
                    ModeleId = 3, // Peugeot 208
                    Annee = 2019,
                    Kilometrage = 45000,
                    Prix = 12800M,
                    DateAcquisition = DateTime.Now.AddDays(-15),
                    Description = "Peugeot 208 avec GPS intégré, climatisation, capteurs de recul.",
                    Statut = StatutVoiture.EnReparation
                }
            };

            context.Voitures.AddRange(voitures);
            context.SaveChanges();

            // Ajouter des photos
            var photos = new PhotoVoiture[]
            {
                new PhotoVoiture
                {
                    VoitureId = 1,
                    CheminImage = "/images/voitures/corolla_1.jpg",
                    Titre = "Toyota Corolla - Vue avant",
                    EstPrincipale = true
                },
                new PhotoVoiture
                {
                    VoitureId = 1,
                    CheminImage = "/images/voitures/corolla_1.jpg",
                    Titre = "Toyota Corolla - Vue arrière",
                    EstPrincipale = false
                },
                new PhotoVoiture
                {
                    VoitureId = 2,
                    CheminImage = "/images/voitures/clio_1.jpg",
                    Titre = "Renault Clio - Vue avant",
                    EstPrincipale = true
                },
                new PhotoVoiture
                {
                    VoitureId = 3,
                    CheminImage = "/images/voitures/clio_1.jpg",
                    Titre = "Peugeot 208 - Vue avant",
                    EstPrincipale = true
                }
            };

            context.PhotosVoitures.AddRange(photos);
            context.SaveChanges();
        }
    }
}