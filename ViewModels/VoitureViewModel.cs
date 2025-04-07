using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMGANSA.Models
{
    public class VoitureViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Veuillez sélectionner un modèle")]
        [Display(Name = "Modèle")]
        public int ModeleId { get; set; }
        
        [Required(ErrorMessage = "L'année de fabrication est obligatoire")]
        [Range(2018, 2100, ErrorMessage = "L'année doit être 2018 ou plus récente")]
        [Display(Name = "Année")]
        public int Annee { get; set; }
        
        [Required(ErrorMessage = "Le kilométrage est obligatoire")]
        [Range(0, 500000, ErrorMessage = "Le kilométrage doit être entre 0 et 500 000 km")]
        [Display(Name = "Kilométrage (km)")]
        public int Kilometrage { get; set; }
        
        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0, 10000000, ErrorMessage = "Le prix doit être entre 0 et 10 000 000")]
        [Display(Name = "Prix (€)")]
        public decimal Prix { get; set; }
        
        [Required(ErrorMessage = "La date d'acquisition est obligatoire")]
        [DataType(DataType.Date)]
        [Display(Name = "Date d'acquisition")]
        public DateTime DateAcquisition { get; set; } = DateTime.Today;
        
        [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Le statut de la voiture est obligatoire")]
        [Display(Name = "Statut")]
        public StatutVoiture Statut { get; set; } = StatutVoiture.Disponible;
        
        [Display(Name = "Photos")]
        public List<IFormFile>? Photos { get; set; }
        
        public List<int>? PhotosASupprimer { get; set; }
    }
}