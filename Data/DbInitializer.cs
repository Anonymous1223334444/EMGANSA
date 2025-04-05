using EMGANSA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMGANSA.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // S'assurer que la base de données est créée
            context.Database.EnsureCreated();

            // Création des rôles
            string[] roleNames = { "Administrateur", "Utilisateur" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Création d'un utilisateur administrateur
            var adminEmail = "admin@emgvoitures.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nom = "Admin",
                    Prenom = "EMG",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrateur");
                }
            }

            // Votre code existant pour initialiser les voitures, si nécessaire
            if (!context.Voitures.Any())
            {
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
}