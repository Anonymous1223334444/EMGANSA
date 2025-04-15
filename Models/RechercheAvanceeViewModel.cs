using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMGANSA.Models
{
    public class RechercheAvanceeViewModel
    {
        [Display(Name = "Marque")]
        public int? MarqueId { get; set; }
        
        [Display(Name = "Modèle")]
        public int? ModeleId { get; set; }
        
        [Display(Name = "Année min")]
        [Range(2018, 2100, ErrorMessage = "L'année minimum est 2018")]
        public int? AnneeMin { get; set; }
        
        [Display(Name = "Année max")]
        [Range(2018, 2100, ErrorMessage = "L'année minimum est 2018")]
        public int? AnneeMax { get; set; }
        
        [Display(Name = "Prix min")]
        [Range(0, 1000000, ErrorMessage = "Le prix doit être positif")]
        public decimal? PrixMin { get; set; }
        
        [Display(Name = "Prix max")]
        [Range(0, 1000000, ErrorMessage = "Le prix doit être positif")]
        public decimal? PrixMax { get; set; }
        
        [Display(Name = "Kilométrage max")]
        [Range(0, 500000, ErrorMessage = "Le kilométrage doit être entre 0 et 500 000 km")]
        public int? KilometrageMax { get; set; }
        
        [Display(Name = "Statut")]
        public StatutVoiture? Statut { get; set; }
        
        [Display(Name = "Tri par")]
        public string? TriPar { get; set; }
        
        [Display(Name = "Ordre")]
        public string? Ordre { get; set; }
        
        // Propriétés pour les listes déroulantes
        public SelectList? Marques { get; set; }
        public SelectList? Modeles { get; set; }
        public SelectList? Statuts { get; set; }
        
        // Propriété pour les résultats
        public List<Voiture>? Resultats { get; set; }
    }
}