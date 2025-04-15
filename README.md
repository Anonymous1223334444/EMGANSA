# EMGANSA - Site Web EMG Voitures

## Description
EMG Voitures est une application web développée avec ASP.NET Core qui permet de gérer un parc automobile. L'application permet d'afficher les voitures de l'inventaire, d'ajouter de nouvelles voitures, de modifier les annonces en cours et de marquer les voitures comme vendues ou non disponibles.

## Fonctionnalités
- **Affichage des voitures** : Consultation de l'inventaire des voitures avec filtrage par statut et recherche textuelle
- **Recherche avancée** : Recherche de voitures selon différents critères (marque, modèle, année, prix, kilométrage, etc.)
- **Gestion des voitures** : Ajout, modification et suppression de voitures (administrateurs uniquement)
- **Gestion des photos** : Upload et gestion des photos pour chaque voiture
- **Gestion des statuts** : Marquage des voitures comme vendues, réservées, en réparation, etc.
- **Tableau de bord** : Visualisation de la répartition des voitures par statut
- **Authentification** : Système de connexion avec différents niveaux d'accès (administrateur, utilisateur)
- **API sécurisée** : API REST avec authentification par JWT pour l'intégration avec d'autres systèmes

## Technologies utilisées
- ASP.NET Core MVC
- Entity Framework Core
- Microsoft Identity
- JWT pour l'authentification
- Bootstrap pour le frontend

## Prérequis
- .NET SDK 8.0 ou version ultérieure
- Visual Studio 2022 ou Visual Studio Code
- SQL Server (LocalDB ou Express)

## Installation
1. Cloner le dépôt :
```dotnet
git clone https://github.com/Anonymous1223334444/EMGANSA.git
```

2. Naviguer vers le dossier du projet :
```dotnet
cd EMGANSA
 ```

3. Restaurer les packages :
```dotnet
dotnet restore
```

4. Exécuter l'application :
```dotnet
dotnet run
```

5. Ouvrir un navigateur et accéder à http://localhost:5189

## Structure du projet
- **Controllers/** : Contrôleurs MVC
- **Models/** : Classes de modèles
- **Views/** : Vues Razor
- **Data/** : Contextes de base de données et migrations
- **Services/** : Services d'application
- **ViewModels/** : Modèles de vue
- **wwwroot/** : Ressources statiques (CSS, JS, images)

## Utilisateurs par défaut
L'application est préconfiguré avec un compte administrateur:
- Email: admin@emgvoitures.com
- Mot de passe: Admin123!

## Contribution
Les contributions sont les bienvenues! N'hésitez pas à ouvrir une issue ou à soumettre une pull request.

## Auteur
Développé par Andre Sarr dans le cadre du cours DIT-INFO-2, École Supérieure Polytechnique (ESP).

## Licence
Ce projet est sous licence MIT.
