# EMGANSA - Site Web EMG Voitures

## Description
Application web pour EMG Voitures, une entreprise qui achète, répare et revend des voitures avec bénéfice. Le site permet de gérer l'inventaire des voitures, d'ajouter de nouvelles voitures, de modifier les annonces existantes et de marquer les voitures comme vendues.

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

## Technologies utilisées
- ASP.NET Core MVC
- Entity Framework Core
- Microsoft Identity
- JWT pour l'authentification
- Bootstrap pour le frontend